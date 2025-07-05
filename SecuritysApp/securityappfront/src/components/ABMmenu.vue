<template>
  <v-container>
    <v-card class="pa-4" outlined>
      <v-card-title>Crear nuevo menú</v-card-title>
      <v-card-text>
        <v-form @submit.prevent="crearMenu">
          <v-text-field v-model="form.nombre" label="Nombre" required></v-text-field>
          <v-text-field v-model="form.ruta" label="Ruta (ej: /nueva-vista)" required></v-text-field>
          <v-text-field v-model="form.componente" label="Nombre del componente (ej: VistaGenerica)"></v-text-field>
          <v-text-field v-model="form.icono" label="Ícono (ej: mdi-view-dashboard)"></v-text-field>
          <v-text-field v-model="form.color" label="Color"></v-text-field>
          <v-select v-model="form.menuPadreId" :items="padres" item-title="nombre" item-value="menuId"
            label="Menú padre (opcional)" return-object></v-select>
          <v-switch v-model="form.visible" label="Visible"></v-switch>
          <v-btn type="submit" color="primary">Crear menú</v-btn>
        </v-form>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
import MenuService from '../services/MenuService'

export default {
  name: 'ABMmenu',
  data() {
    return {
      form: {
        nombre: null,
        ruta: '',
        componente: '',
        icono: '',
        color: '',
        orden: 0,
        menuPadreId: null,
        visible: true,
        sistemaId: 1,
        carpeta: ''
      },
      padres: []
    }
  },
  mounted() {
    MenuService.obtenerPadres().then(res => this.padres = res)
  },
  methods: {
    getNombrePadre() {
      if (!this.form.menuPadreId) return ''
      const padre = this.padres.find(p => p.menuId === this.form.menuPadreId)
      return padre ? padre.nombre : ''
    },

    async crearMenu() {
      try {
        // 🧠 Convertir objeto padre a ID
        if (this.form.menuPadreId && typeof this.form.menuPadreId === 'object') {
          this.form.menuPadreId = this.form.menuPadreId.menuId;
        } else if (!this.form.menuPadreId) {
          this.form.menuPadreId = 0;
        }

        // 🔢 Obtener orden del backend
        const ultimoOrden = await MenuService.obtenerUltimoOrden(this.form.menuPadreId || 0);
        this.form.orden = ultimoOrden;

        // 🎯 Valores por defecto
        if (!this.form.icono) this.form.icono = 'mdi-menu';
        if (!this.form.componente && this.form.menuPadreId === 0) {
          this.form.componente = 'folder';
        }

        // ✅ Si es solo carpeta, asignar directamente y evitar fetch
        if (this.form.componente === 'folder') {
          this.form.carpeta = this.form.nombre;

        } else {
          // 📂 Crear la vista y obtener la carpeta
          const vistaRes = await fetch(
            `http://localhost:3000/generar-vista/${this.form.componente}?MenuPadreId=${this.form.menuPadreId}&Nombre=${this.form.nombre}&CarpetaPadre=${this.getNombrePadre()}`
          );

          if (!vistaRes.ok) {
            const errorText = await vistaRes.text();
            throw new Error(`❌ Error HTTP ${vistaRes.status}: ${errorText}`);
          }

          const respuestaVista = await vistaRes.json();
          console.log('📥 Respuesta del servidor:', respuestaVista);

          this.form.carpeta = respuestaVista.carpeta || this.getNombrePadre() || 'SinCarpeta';
        }

        // 📦 Fallback adicional desde localStorage
        if (!this.form.carpeta) {
          this.form.carpeta = localStorage.getItem(`carpeta_${this.form.nombre}`) || this.getNombrePadre() || 'SinCarpeta';
        }

        console.log(`🛣️ Ruta esperada: src/views/${this.form.carpeta}/${this.form.componente}.vue`);

        // 🧾 Payload listo
        const payload = { ...this.form };
        if (!payload.color) delete payload.color;

        console.log('📝 Payload final a insertar:', payload);

        // 💾 Insertar en BD
        await MenuService.insertar(payload);

        // 🗃️ Guardar carpeta en localStorage
        localStorage.setItem(`carpeta_${this.form.nombre}`, this.form.carpeta);

        alert('✅ Menú y vista creados con éxito');

        window.location.reload();

      } catch (e) {
        console.error('❌ Error en el flujo de creación de menú o vista:', e);
        alert('Ocurrió un error al crear el menú o la vista');
      }
    }

  }
}
</script>

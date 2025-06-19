<template>
  <v-container>
    <v-card class="pa-4" outlined>
      <v-card-title>Crear nuevo menú</v-card-title>
      <v-card-text>
        <v-form @submit.prevent="crearMenu">
          <v-text-field v-model="form.nombre" label="Nombre" required></v-text-field>
          <v-text-field v-model="form.ruta" label="Ruta (ej: /nueva-vista)" required></v-text-field>
          <v-text-field v-model="form.componente" label="Nombre del componente (ej: VistaGenerica)" required></v-text-field>
          <v-text-field v-model="form.icono" label="Ícono (ej: mdi-view-dashboard)"></v-text-field>
          <v-text-field v-model="form.color" label="Color"></v-text-field>
          <v-select
            v-model="form.menuPadreId"
            :items="padres"
            item-title="nombre"
            item-value="menuId"
            label="Menú padre (opcional)"
            return-object
          ></v-select>
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
        nombre: '',
        ruta: '',
        componente: '',
        icono: '',
        color: '',
        orden: 0,
        menuPadreId: null,
        visible: true,
        sistemaId: 1
      },
      padres: []
    }
  },
  mounted() {
    MenuService.obtenerPadres().then(res => this.padres = res)
  },
  methods: {
    async crearMenu() {
      try {
        // Normalizar datos
        if (this.form.menuPadreId && typeof this.form.menuPadreId === 'object') {
          this.form.menuPadreId = this.form.menuPadreId.menuId
        }

        const ultimoOrden = await MenuService.obtenerUltimoOrden(this.form.menuPadreId || 0)
        this.form.orden = ultimoOrden

        // Asignar ícono por defecto si está vacío
        if (!this.form.icono) this.form.icono = 'mdi-menu'

        // Crear objeto limpio para insertar
        const payload = { ...this.form }
        if (!payload.color) delete payload.color
        if (!payload.componente) delete payload.componente

        console.log('✅ Enviando datos al backend para insertar menú:', payload)

        await MenuService.insertar(payload)

        console.log('📤 Menú insertado correctamente, generando vista...')

        // Generar vista con servidor Node.js
        await fetch(`http://localhost:3000/generar-vista/${this.form.componente}`)

        alert('✅ Menú y vista creados con éxito')
        window.location.reload()

      } catch (e) {
        console.error('❌ Error en el flujo de creación de menú o vista:', e)
        alert('Ocurrió un error al crear el menú o la vista')
      }
    }
  }
}
</script>

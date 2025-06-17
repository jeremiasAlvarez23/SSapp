<template>
    <v-container>
        <v-card class="pa-4">
            <v-card-title>Gestión de Menús</v-card-title>
            <v-form @submit.prevent="guardarMenu">
                <v-row>
                    <v-col cols="12" md="6">
                        <v-text-field label="Nombre" v-model="form.nombre" required></v-text-field>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-text-field label="Ruta" v-model="form.ruta"></v-text-field>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-text-field label="Componente" v-model="form.componente"></v-text-field>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-text-field label="Ícono (mdi-*)" v-model="form.icono"></v-text-field>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-text-field label="Color" v-model="form.color"></v-text-field>
                    </v-col>
                    <v-col cols="12" md="3">
                        <v-text-field label="Orden" v-model.number="form.orden" type="number"></v-text-field>
                    </v-col>
                    <v-col cols="12" md="3">
                        <v-switch label="Visible" v-model="form.visible"></v-switch>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-select label="Menú Padre" :items="menuPadres" item-text="nombre" item-value="menuId"
                            v-model="form.menuPadreId" return-object persistent-hint
                            hint="Opcional: selecciona un menú padre"></v-select>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-text-field label="Sistema ID" v-model.number="form.sistemaId" required></v-text-field>
                    </v-col>
                    <v-col cols="12">
                        <v-btn type="submit" color="primary">Guardar Menú</v-btn>
                    </v-col>
                </v-row>
            </v-form>
        </v-card>

        <v-card class="mt-4">
            <v-card-title>Listado de Menús</v-card-title>
            <v-data-table :headers="headers" :items="menuList" item-value="menuId">
                <template v-slot:[`item.actions`]="{ item }">
                    <v-btn icon @click="editarMenu(item)"><v-icon>mdi-pencil</v-icon></v-btn>
                    <v-btn icon @click="eliminarMenu(item.menuId)" color="red"><v-icon>mdi-delete</v-icon></v-btn>
                </template>

            </v-data-table>
        </v-card>
    </v-container>
</template>

<script>
import MenuService from '@/services/MenuService';

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
                orden: 1,
                visible: true,
                menuPadreId: 0,
                sistemaId: 1,
            },
            menuPadres: [],
            menuList: [],
            headers: [
                { text: 'Nombre', value: 'nombre' },
                { text: 'Ruta', value: 'ruta' },
                { text: 'Orden', value: 'orden' },
                { text: 'Visible', value: 'visible' },
                { text: 'Acciones', value: 'actions', sortable: false },
            ],
        };
    },
    async created() {
        await this.cargarMenus();
        await this.cargarMenuPadres();
    },
    methods: {
        async cargarMenus() {
            try {
                const res = await MenuService.obtenerTodo();
                this.menuList = res;
            } catch (e) {
                console.error('Error al cargar menús', e);
            }
        },
        async cargarMenuPadres() {
            try {
                const res = await MenuService.obtenerPadres();
                this.menuPadres = res;
            } catch (e) {
                console.error('Error al cargar padres', e);
            }
        },
        async guardarMenu() {
            try {
                const payload = { ...this.form, menuPadreId: this.form.menuPadreId?.menuId || 0 };
                await MenuService.insertar(payload);
                this.resetForm();
                await this.cargarMenus();
                this.$toast.success('Menú guardado correctamente');
            } catch (e) {
                console.error(e);
                this.$toast.error('Error al guardar el menú');
            }
        },
        editarMenu(item) {
            this.form = { ...item };
        },
        async eliminarMenu(id) {
            try {
                await MenuService.eliminar(id);
                await this.cargarMenus();
                this.$toast.success('Menú eliminado');
            } catch (e) {
                console.error(e);
                this.$toast.error('Error al eliminar menú');
            }
        },
        resetForm() {
            this.form = {
                nombre: '', ruta: '', componente: '', icono: '', color: '', orden: 1,
                visible: true, menuPadreId: 0, sistemaId: 1,
            };
        },
    },
};
</script>

<style scoped>
.v-card-title {
    font-weight: bold;
}
</style>

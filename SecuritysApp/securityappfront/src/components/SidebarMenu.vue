<template>
    <v-navigation-drawer v-model="drawer" app clipped dark color="primary">
        <v-list dense nav>
            <template v-for="item in items" :key="item.menuId">
                <!-- Si tiene submenú -->
                <v-list-group v-if="item.submenu && item.submenu.length" :prepend-icon="item.icono" no-action>
                    <template v-slot:activator>
                        <v-list-item-content>
                            <v-list-item-title>{{ item.nombre }}</v-list-item-title>
                        </v-list-item-content>
                    </template>

                    <v-list-item v-for="sub in item.submenu" :key="sub.menuId" :to="sub.ruta" link>
                        <v-list-item-icon>
                            <v-icon>{{ sub.icono }}</v-icon>
                        </v-list-item-icon>
                        <v-list-item-content>
                            <v-list-item-title>{{ sub.nombre }}</v-list-item-title>
                        </v-list-item-content>
                    </v-list-item>
                </v-list-group>

                <!-- Si no tiene submenú -->
                <v-list-item v-else :to="item.ruta" link>
                    <v-list-item-icon>
                        <v-icon>{{ item.icono }}</v-icon>
                    </v-list-item-icon>
                    <v-list-item-content>
                        <v-list-item-title>{{ item.nombre }}</v-list-item-title>
                    </v-list-item-content>
                </v-list-item>
            </template>
        </v-list>
    </v-navigation-drawer>
</template>

<script>
import MenuService from "../services/MenuService";

export default {
    name: "SidebarMenu",
    data() {
        return {
            drawer: true,
            items: [],
        };
    },
    async created() {
        try {
            const res = await MenuService.obtenerPorUsuario();
            this.items = res.data;
        } catch (error) {
            console.error("Error al cargar el menú:", error);
        }
    },
};
</script>

<style scoped>
.v-navigation-drawer {
    width: 260px;
}
</style>

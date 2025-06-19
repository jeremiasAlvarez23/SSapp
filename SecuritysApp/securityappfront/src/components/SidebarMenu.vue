<template>
    <v-navigation-drawer v-model="internalDrawer" app color="#1e1e2f" width="260">
        <v-list dense nav>
            <!-- Ítem fijo: acceso al ABM de menú -->
            <v-list-item to="/abm-menu" prepend-icon="mdi-cog" title="ABM Menú" class="mt-2" />

            <v-divider class="my-2" />

            <!-- Menús dinámicos -->
            <template v-for="menu in items" :key="menu.menuId">
                <!-- Si tiene submenús -->
                <v-list-group v-if="menu.submenu && menu.submenu.length > 0" :value="false" no-action>
                    <template #activator>
                        <v-list-item-title>
                            <v-icon start>{{ menu.icono || 'mdi-folder' }}</v-icon>
                            {{ menu.nombre }}
                        </v-list-item-title>
                    </template>

                    <v-list-item v-for="sub in menu.submenu" :key="sub.menuId" :to="sub.ruta" link>
                        <v-icon start>{{ sub.icono || 'mdi-file' }}</v-icon>
                        <v-list-item-title>{{ sub.nombre }}</v-list-item-title>
                    </v-list-item>
                </v-list-group>

                <!-- Si NO tiene submenús -->
                <v-list-item v-else :to="menu.ruta" link>
                    <v-icon start>{{ menu.icono || 'mdi-menu' }}</v-icon>
                    <v-list-item-title>{{ menu.nombre }}</v-list-item-title>
                </v-list-item>
            </template>
        </v-list>
    </v-navigation-drawer>
</template>

<script>
export default {
    name: 'SidebarMenu',
    props: {
        modelValue: {
            type: Boolean,
            default: true
        },
        items: {
            type: Array,
            required: true
        }
    },
    computed: {
        internalDrawer: {
            get() {
                return this.modelValue
            },
            set(val) {
                this.$emit('update:modelValue', val)
            }
        }
    }
}
</script>

<style scoped>
.v-navigation-drawer {
    background-color: #1e1e2f;
    color: #ffffff;
}
</style>

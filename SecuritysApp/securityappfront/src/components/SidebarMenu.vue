<template>
  <v-navigation-drawer app color="deep-purple" dark>
    <v-list dense>
      <v-list-item>
        <v-list-item-title class="white--text font-weight-bold">SSApp</v-list-item-title>
      </v-list-item>

      <!-- Menús con hijos -->
      <v-list-group
        v-for="menuPadre in menusConHijos"
        :key="menuPadre.menuId"
        :prepend-icon="menuPadre.icono || 'mdi-menu-right'"
      >
        <template v-slot:activator="{ props }">
          <v-list-item v-bind="props">
            <v-list-item-title>{{ menuPadre.nombre }}</v-list-item-title>
          </v-list-item>
        </template>

        <v-list-item
          v-for="submenu in obtenerSubmenus(menuPadre.menuId)"
          :key="submenu.menuId"
          :to="submenu.ruta"
          link
          router
          class="pl-6"
        >
          <v-list-item-title>{{ submenu.nombre }}</v-list-item-title>
        </v-list-item>
      </v-list-group>

      <!-- Menús sin hijos -->
      <v-list-item
        v-for="menu in menusIndependientes"
        :key="menu.menuId"
        :to="menu.ruta"
        link
        router
      >
        <v-list-item-title>{{ menu.nombre }}</v-list-item-title>
      </v-list-item>
    </v-list>
  </v-navigation-drawer>
</template>

<script>
import MenuService from "../services/MenuService";

export default {
  data() {
    return {
      menus: [],
    };
  },
  computed: {
    menusConHijos() {
      return this.menus.filter(
        m => m.menuPadreId === 0 &&
        this.menus.some(h => h.menuPadreId === m.menuId && h.ruta && h.componente)
      );
    },
    menusIndependientes() {
      return this.menus.filter(
        m =>
          m.menuPadreId === 0 &&
          !this.menus.some(h => h.menuPadreId === m.menuId && h.ruta && h.componente) &&
          m.ruta &&
          m.componente
      );
    }
  },
  methods: {
    obtenerSubmenus(padreId) {
      return this.menus.filter(m => m.menuPadreId === padreId && m.ruta && m.componente);
    },
    async cargarMenus() {
      try {
        const response = await MenuService.obtenerTodo();
        console.log("MENÚS DESDE BACKEND:", response);
        this.menus = response;
      } catch (error) {
        console.error("Error al cargar menús:", error);
      }
    },
  },
  created() {
    this.cargarMenus();
  },
};
</script>

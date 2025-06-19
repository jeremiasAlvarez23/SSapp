<template>
  <v-app class="css-cube-background">
    <!-- Header que emite toggleSidebar -->
    <Header @toggleSidebar="drawer = !drawer" />

    <!-- Sidebar recibe el valor del drawer + items del backend -->
    <Sidebar v-model="drawer" :items="menuItems" />

    <!-- Contenido principal -->
    <v-main class="pa-4" :style="{
      marginTop: '64px',
      marginLeft: drawer ? '260px' : '0px',
      transition: 'margin-left 0.3s ease'
    }">
      <router-view />
    </v-main>
  </v-app>
</template>

<script>
import Header from '../components/HeaderBar.vue'
import Sidebar from '../components/SidebarMenu.vue'
import MenuService from '../services/MenuService'

export default {
  components: {
    Header,
    Sidebar
  },
  data() {
    return {
      drawer: true,
      menuItems: []
    }
  },
  async mounted() {
    try {
      const resultado = await MenuService.obtenerPorUsuario()
      this.menuItems = resultado
    } catch (error) {
      console.error('❌ Error al obtener menús:', error)
    }
  }
}
</script>

<style>
.css-cube-background {
  background-color: #0a0a0a;
  background-image:
    conic-gradient(from 135deg at 50% 50%, #1a1a1a 25%, #000 0 50%, #1a1a1a 0 75%, #000 0),
    conic-gradient(from 135deg at 50% 50%, #111 25%, #000 0 50%, #111 0 75%, #000 0);
  background-size: 100px 100px;
  background-position: 0 0, 50px 50px;
}
</style>

<template>
    <v-app class="css-cube-background">
        <Header @toggleSidebar="drawer = !drawer" />
        <Sidebar v-model="drawer" />

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
import Header from '@/components/HeaderBar.vue'
import Sidebar from '@/components/SidebarMenu.vue'

export default {
    components: {
        Header,
        Sidebar
    },
    data() {
        return {
            drawer: true
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

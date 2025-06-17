<template>
    <v-app-bar class="headerstyle" flat app height="64">
        <v-app-bar-nav-icon @click="$emit('toggleSidebar')" />

        <v-toolbar-title class="ml-2">Panel Principal</v-toolbar-title>

        <v-spacer></v-spacer>

        <v-btn icon @click="logout" title="Cerrar sesión">
            <v-icon>mdi-logout</v-icon>
        </v-btn>
    </v-app-bar>
</template>

<script>
export default {
    methods: {
        logout() {
            localStorage.removeItem('token')
            localStorage.removeItem('usuario')
            this.$router.push('/')
        }
    }
}
</script>


<style scoped>
.headerstyle {
    background-color: #ff0000;
    color: #000000;
}
</style>
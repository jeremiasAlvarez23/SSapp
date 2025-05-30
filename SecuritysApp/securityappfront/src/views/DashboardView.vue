<template>
  <v-container>
    <h1>Bienvenido, {{ nombreUsuario }}</h1>
    <v-btn color="error" @click="logout">Cerrar sesión</v-btn>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      nombreUsuario: ''
    }
  },
  mounted() {
    const usuario = JSON.parse(localStorage.getItem('usuario'))
    if (usuario) {
      this.nombreUsuario = usuario.nombre
    }
  },
  methods: {
    logout() {
      localStorage.removeItem('token')
      localStorage.removeItem('usuario')
      this.$router.push('/')
    }
  }
}
</script>

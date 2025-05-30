<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" sm="8" md="4">
        <v-card>
          <v-card-title class="text-h5">Iniciar Sesión</v-card-title>
          <v-card-text>
            <v-text-field label="Email" v-model="email" outlined></v-text-field>
            <v-text-field label="Clave" v-model="clave" type="password" outlined></v-text-field>
          </v-card-text>
          <v-card-actions>
            <v-btn color="primary" @click="login">Entrar</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import AuthService from '@/services/AuthService'

export default {
  data() {
    return {
      email: '',
      clave: ''
    }
  },
  methods: {
  async login() {
    try {
      const data = await AuthService.login(this.email, this.clave)
      localStorage.setItem('token', data.token)
      localStorage.setItem('usuario', JSON.stringify({
        id: data.usuarioId,
        nombre: data.nombre,
        email: data.email,
        sistemas: data.sistemasPermitidos
      }))
      this.$router.push('/dashboard') // Redirige al dashboard
    } catch (error) {
      alert(error.message || 'Error al iniciar sesión')
    }
  }
}

}
</script>

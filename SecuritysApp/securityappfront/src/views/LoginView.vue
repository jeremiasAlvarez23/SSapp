<template>
  <v-container fluid class="fill-height d-flex align-center justify-center login-background">
    <!-- Card trasera -->
    <div class="back-card"></div>

    <!-- Card frontal con login -->
    <v-card class="front-card" elevation="12">
      <!-- Avatar -->
      <div class="avatar-wrapper">
        <v-avatar size="90" class="avatar-circle">
          <v-icon size="50" color="white">mdi-account</v-icon>
        </v-avatar>
      </div>

      <v-card-text class="card-content">
        <v-form @submit.prevent="login" class="form-content">
          <!-- Inputs personalizados -->
          <div class="inputs-container">
            <!-- Email -->
            <div class="input-wrapper">
              <label for="email">Email</label>
              <div class="input-icon">
                <i class="mdi mdi-email input-icon-left"></i>
                <input type="email" id="email" v-model="email" required />
              </div>
            </div>

            <!-- Password -->
            <div class="input-wrapper">
              <label for="clave">Contraseña</label>

              <div class="input-icon">
                <i class="mdi mdi-lock input-icon-left"></i>
                <input :type="showPassword ? 'text' : 'password'" id="clave" v-model="clave" required />
                <i :class="['mdi', showPassword ? 'mdi-eye-off' : 'mdi-eye', 'toggle-password']"
                  @click="showPassword = !showPassword"></i>
              </div>

            </div>
          </div>
        </v-form>

        <div style="display: flex; justify-content: center;">
          <v-btn @click="login" color="deep-orange accent-4" block class="submit-btn">
            Ingresar
          </v-btn>
        </div>

      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
import AuthService from '../services/AuthService'

export default {
  data() {
    return {
      email: '',
      clave: '',
      showPassword: false
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

        this.$router.push('/dashboard')
      } catch (error) {
        console.error('Error al iniciar sesión:', error)
        alert(error.message || 'Error al iniciar sesión')
      }
    }
  }
}
</script>

<style scoped>
.login-background {
  background-image: url('../assets/fondo.png');
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
}

.back-card {
  position: absolute;
  width: 450px;
  height: 700px;
  background-image: url('../assets/Hexagon.svg');
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  z-index: 1;
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.568);
}


.front-card {
  width: 340px;
  height: 460px;
  z-index: 2;
  position: relative;
  background-color: white;
  overflow: visible;
  display: flex;
  flex-direction: column;
}

.avatar-wrapper {
  position: absolute;
  top: -45px;
  left: 50%;
  transform: translateX(-50%);
  z-index: 3;
}

.avatar-circle {
  background-color: #609cbe;
  border: 3px solid white;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
}

.card-content {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding: 32px;
  height: 100%;
}

.form-content {
  display: flex;
  flex-direction: column;
  justify-content: center;
  height: 100%;
}

.inputs-container {
  display: flex;
  flex-direction: column;
  gap: 26px;
  padding-bottom: 20px;
}

.submit-btn {
  height: 60px;
  font-size: 0.9rem;
  font-weight: 600;
  text-transform: uppercase;
  letter-spacing: 1px;
}


.input-wrapper {
  margin-bottom: 20px;
  display: flex;
  flex-direction: column;
}

.input-wrapper label {
  margin-bottom: 6px;
  font-weight: 600;
  color: #333;
}

.input-icon {
  position: relative;
  display: flex;
  align-items: center;
}

.input-icon input {
  width: 100%;
  padding: 10px 38px 10px 38px;
  /* espacio a izquierda y derecha */
  border: 2px solid #ccc;
  border-radius: 8px;
  font-size: 16px;
  outline: none;
  transition: 0.3s;
  background-color: #e8f1fd;
}

.input-icon-left {
  position: absolute;
  left: 10px;
  color: #609cbe;
  font-size: 20px;
}

.toggle-password {
  position: absolute;
  right: 10px;
  color: #609cbe;
  font-size: 20px;
  cursor: pointer;
}
</style>

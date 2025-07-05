import { createApp } from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import router, { inicializarRutas } from './router'

async function iniciarApp() {
  try {
    await inicializarRutas()
    createApp(App)
      .use(vuetify)
      .use(router)
      .mount('#app')
  } catch (e) {
    console.error('❌ Error al iniciar app:', e)
  }
}

iniciarApp()

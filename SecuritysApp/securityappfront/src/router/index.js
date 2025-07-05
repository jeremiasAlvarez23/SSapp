import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/LoginView.vue'
import DashboardView from '../views/DashboardView.vue'
import DefaultLayout from '../views/DefaultLayout.vue'
import ABMmenu from '../components/ABMmenu.vue'
import MenuService from '../services/MenuService'
import { generarRutasDesdeMenu } from './RouterLoader'

// ⚙️ Rutas estáticas (comunes)
const routes = [
  {
    path: '/',
    name: 'Login',
    component: LoginView
  },
  {
    path: '/',
    name: 'DefaultLayout',
    component: DefaultLayout,
    meta: { requiresAuth: true },
    children: [
      {
        path: '/dashboard',
        name: 'Dashboard',
        component: DashboardView
      },
      {
        path: '/abm-menu',
        name: 'ABMmenu',
        component: ABMmenu
      }
    ]
  }
]

// 🌐 Instancia del router
const router = createRouter({
  history: createWebHistory(),
  routes
})

// 🔐 Guard de autenticación
router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token')
  if (to.meta.requiresAuth && !token) {
    next('/')
  } else {
    next()
  }
})

// 🚀 Esta función se usará en main.js para esperar la carga dinámica
export async function inicializarRutas() {
  try {
    const menus = await MenuService.obtenerPorUsuario()
    const rutas = generarRutasDesdeMenu(menus)

    rutas.forEach(ruta => {
      router.addRoute('DefaultLayout', ruta)
    })

    console.log('🧭 Rutas dinámicas cargadas:', rutas)
  } catch (e) {
    console.error('❌ Error cargando rutas dinámicas:', e)
  }
}

export default router

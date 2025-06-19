import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/LoginView.vue'
import DashboardView from '../views/DashboardView.vue'
import DefaultLayout from '../views/DefaultLayout.vue'
import { generarRutasDesdeMenu } from './RouterLoader'
import MenuService from '../services/MenuService'
import ABMmenu from '../components/ABMmenu.vue'

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
      },
    ]
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token')
  if (to.meta.requiresAuth && !token) {
    next('/')
  } else {
    next()
  }
})

MenuService.obtenerPorUsuario().then(menus => {
  const rutas = generarRutasDesdeMenu(menus)
  rutas.forEach(ruta => router.addRoute('DefaultLayout', ruta))
})


export default router

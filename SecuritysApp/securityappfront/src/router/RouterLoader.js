export function generarRutasDesdeMenu(menus) {
  const rutas = []

  menus.forEach(menu => {
    rutas.push({
      path: menu.ruta,
      name: menu.nombre,
      // 👇 IMPORT DINÁMICO desde carpeta Dinamicas
      component: () => import(`@/views/Dinamicas/${menu.componente}.vue`).catch(() => import('@/views/Dinamicas/VistaGenerica.vue')),
      meta: { requiresAuth: true }
    })

    if (menu.submenu?.length > 0) {
      rutas.push(...generarRutasDesdeMenu(menu.submenu))
    }
  })

  return rutas
}

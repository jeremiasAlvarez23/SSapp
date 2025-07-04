const context = require.context('@/views/', true, /\.vue$/)

const vistas = {}
context.keys().forEach(key => {
  vistas[key] = context(key).default
})

export function generarRutasDesdeMenu(menus) {
  const rutas = []

  menus.forEach(menu => {
    const carpeta = menu.carpeta 
    const rutaEsperada = `./${carpeta}/${menu.componente}.vue`

    const componente = vistas[rutaEsperada] 

    rutas.push({
      path: menu.ruta,
      name: menu.nombre,
      component: componente,
      meta: {
        requiresAuth: true
      }
    })

    if (menu.submenu?.length > 0) {
      rutas.push(...generarRutasDesdeMenu(menu.submenu))
    }
  })

  return rutas
}

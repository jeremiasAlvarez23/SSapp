// src/router/RouterLoader.js
import ABMmenu from '../components/ABMmenu.vue'
// Importá más componentes si lo necesitás

const componentes = {
    ABMmenu,
    // otros: OtrasView, etc.
}

export function generarRutasDesdeMenu(menus) {
    return menus.map(menu => {
        return {
            path: menu.ruta,
            name: menu.nombre,
            component: componentes[menu.componente],
            meta: { requiresAuth: true }
        }
    })
}

const context = require.context('@/views/', true, /\.vue$/);

const vistas = {};
context.keys().forEach(key => {
  const ruta = key.replace('./', ''); 
  vistas[ruta] = context(key).default;
});

export function generarRutasDesdeMenu(menus) {
  const rutas = [];

  menus.forEach(menu => {
    let carpeta = menu.carpeta || localStorage.getItem(`carpeta_${menu.nombre}`);
    const componente = menu.componente;

    if ((!carpeta || !componente) && !menu.submenu?.length) {
      console.warn(`❌ Falta carpeta o componente en el menú sin hijos:`, menu);
      return;
    }

    if (carpeta && componente) {
      const path = `@/views/${carpeta}/${componente}.vue`;

      rutas.push({
        path: menu.ruta,
        name: menu.nombre,
        component: () =>
          import(/* @vite-ignore */ `@/views/${carpeta}/${componente}.vue`)
            .catch(() => import('@/views/Dinamicas/VistaGenerica.vue')),
        meta: {
          requiresAuth: true
        }
      });

      console.log(`✅ Ruta dinámica registrada: ${menu.ruta} -> ${path}`);
    }

    if (menu.submenu?.length > 0) {
      rutas.push(...generarRutasDesdeMenu(menu.submenu));
    }
  });

  return rutas;
}


const context = require.context('@/views/', true, /\.vue$/);

const vistas = {};
context.keys().forEach(key => {
  const ruta = key.replace('./', ''); 
  vistas[ruta] = context(key).default;
});

export function generarRutasDesdeMenu(menus) {
  const rutas = [];

  menus.forEach(menu => {
    // 🧠 Intentar recuperar carpeta desde el objeto o localStorage
    let carpeta = menu.carpeta;
    const componente = menu.componente;

    if (!carpeta) {
      carpeta = localStorage.getItem(`carpeta_${menu.nombre}`);
    }

    // 🔍 Log útil para depurar cada menú
    console.log(`🧩 Evaluando menú:`, {
      nombre: menu.nombre,
      carpeta,
      componente
    });

    // ❌ Si no hay carpeta ni componente y tampoco hijos, no registrar
    if ((!carpeta || !componente) && !menu.submenu?.length) {
      console.warn(`❌ Falta carpeta o componente en el menú sin hijos:`, menu);
      return;
    }

    // 🧭 Solo crear ruta si ambos están definidos
    if (carpeta && componente) {
      const rutaEsperada = `${carpeta}/${componente}.vue`;
      console.log(`🛣️ Buscando vista en: src/views/${rutaEsperada}`);

      const componenteEncontrado = vistas[rutaEsperada];

      if (!componenteEncontrado) {
        console.warn(`⚠️ Vista no encontrada para: ${rutaEsperada}. No se registrará en el router.`);
      } else {
        rutas.push({
          path: menu.ruta,
          name: menu.nombre,
          component: componenteEncontrado,
          meta: {
            requiresAuth: true
          }
        });

        console.log(`✅ Vista registrada: ${menu.ruta} -> src/views/${rutaEsperada}`);
      }
    }

    // 🧬 Procesar submenús (recursivamente)
    if (menu.submenu?.length > 0) {
      rutas.push(...generarRutasDesdeMenu(menu.submenu));
    }
  });

  return rutas;
}

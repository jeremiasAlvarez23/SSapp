const express = require('express');
const fs = require('fs');
const path = require('path');
const cors = require('cors');

const app = express();
app.use(cors());

app.get('/generar-vista/:componente', (req, res) => {
  const componente = req.params.componente;
  const menuPadreId = parseInt(req.query.MenuPadreId || '0');
  let nombreCarpeta = req.query.Nombre || '';
  const carpetaPadre = req.query.CarpetaPadre || '';
  const ruta = req.query.Ruta || `/ruta-no-definida`;

  // Determinar carpeta final
  if (menuPadreId !== 0 && carpetaPadre) {
    nombreCarpeta = carpetaPadre;
  }

  // Validar que haya una carpeta
  if (!nombreCarpeta) {
    return res.status(400).json({ error: 'Falta el nombre de la carpeta donde guardar la vista.' });
  }

  const viewsPath = path.join(__dirname, '../src/views');
  const carpetaDestino = path.join(viewsPath, nombreCarpeta);

  // ✅ Siempre asegurar que la carpeta exista
  if (!fs.existsSync(carpetaDestino)) {
    fs.mkdirSync(carpetaDestino, { recursive: true });
    console.log('📁 Carpeta creada:', carpetaDestino);
  }

  let mensaje = '✅ Carpeta creada';

  // Crear archivo solo si es una vista (no carpeta raíz tipo "folder")
  if (menuPadreId !== 0 && componente !== 'folder') {
    const archivoVista = path.join(carpetaDestino, `${componente}.vue`);
    
    if (!fs.existsSync(archivoVista)) {
      const contenido = `<template>
  <div>
    <h1>${componente}</h1>
  </div>
</template>

<script>
export default {
  name: '${componente}'
}
</script>

<style scoped>
</style>
`;
      fs.writeFileSync(archivoVista, contenido);
      console.log('📄 Vista creada:', archivoVista);
      mensaje = '✅ Vista hijo creada';
    } else {
      console.log('ℹ️ Vista ya existe:', archivoVista);
      mensaje = 'ℹ️ Vista ya existía';
    }
  }

  console.log('📦 Carpeta final usada:', nombreCarpeta);

  res.json({
    mensaje,
    componente,
    carpeta: nombreCarpeta,
    ruta
  });
});

const PORT = 3000;
app.listen(PORT, () => {
  console.log(`🚀 Servidor corriendo en http://localhost:${PORT}`);
});

const express = require('express');
const fs = require('fs');
const path = require('path');
const cors = require('cors');

const app = express();
app.use(cors());

app.get('/generar-vista/:nombre', (req, res) => {
  const nombre = req.params.nombre;
  const archivo = `${nombre}.vue`;
  const ruta = path.join(__dirname, '../src/views/Dinamicas', archivo);

  if (fs.existsSync(ruta)) {
    return res.status(200).send('Ya existe.');
  }

  const contenido = `<template>
  <v-container>
    <v-card class="pa-4" outlined>
      <v-card-title>${nombre}</v-card-title>
      <v-card-text>
        <p>Esta vista fue generada automáticamente.</p>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
export default {
  name: '${nombre}'
}
</script>
`;

  fs.writeFileSync(ruta, contenido);
  res.status(201).send('Vista creada.');
});

const PORT = 3000;
app.listen(PORT, () => {
  console.log(`🚀 Generador de vistas corriendo en http://localhost:${PORT}`);
});

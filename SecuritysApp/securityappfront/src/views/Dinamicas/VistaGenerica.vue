<template>
  <v-container>
    <v-card class="pa-4" outlined>
      <v-card-title>Vista: {{ $route.name }}</v-card-title>
      <v-card-text>Esta vista fue generada dinámicamente.</v-card-text>
    </v-card>
  </v-container>
</template>

<script>
export default {
  name: 'VistaGenerica'
}
</script>

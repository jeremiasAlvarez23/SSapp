<template>
  <v-app class="color">
    <router-view />
  </v-app>
</template>

<style>
html,
body,
#app {
  height: 100%;
  margin: 0;
}

/* .color {
  background: linear-gradient(135deg, #fffb00, #d16347);
} */

.color {
  background: linear-gradient(135deg,
      #366683 0%,
      #514b66 25%,
      #d4b99d 50%,
      #e6eae2 75%,
      #789bb1 100%);
  background-size: cover;
  background-attachment: fixed;
  filter: blur(0.5px);
}

@keyframes gradientBG {
  0% {
    background-position: 0% 50%;
  }

  50% {
    background-position: 100% 50%;
  }

  100% {
    background-position: 0% 50%;
  }
}
</style>

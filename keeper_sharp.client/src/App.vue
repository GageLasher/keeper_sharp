<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <KeepDetailsModal2 />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState),
      activeKeep: computed(() => AppState.activeKeep)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
</style>

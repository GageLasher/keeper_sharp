<template>
  <div class="row m-5">
    <div class="col-12 d-flex justify-content-between">
      <h1>{{ vault.name }}</h1>
      <button
        class="btn btn-outline-info"
        v-if="vault.creator?.id == account.id"
      >
        Delete Vault
      </button>
    </div>
    <div class="col-12 mt-2">
      Keeps: <span>{{ keeps.length }} </span>
    </div>
  </div>
  <div class="masonry-with-columns ms-5 mb-5 me-5">
    <div v-for="k in keeps" :key="k.id" class="">
      <KeepCard :keep="k" />
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { onMounted } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
import { router } from '../router'
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultsService.getVault(route.params.id)
        await vaultsService.getVaultKeeps(AppState.activeVault.id)
      } catch (error) {
        router.push({ name: 'Home' })

        logger.log(error)
        Pop.toast(error.message)

      }
    })
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    display: inline-block;
    width: 100%;
  }
}
</style>
<template>
  <div class="container-fluid m-0">
    <div class="row m-0">
      <div class="col-12 d-flex mt-5">
        <!-- <div class="row"> -->
        <!-- <div class="col-md-6 m-5 d-flex"> -->
        <img :src="profile?.picture" alt="" class="img-fluid rounded" />
        <div class="row ms-3">
          <h2 class="">{{ profile.name }}</h2>
          <h5>Vaults: {{ profileVaults.length }}</h5>
          <h5>Keeps: {{ profileKeeps.length }}</h5>
          <!-- </div> -->
          <!-- </div> -->
        </div>
      </div>
    </div>
    <div class="row m-5">
      <div class="col-12">
        <h3>Vaults</h3>
      </div>

      <div class="col-12">
        <div class="row">
          <div
            class="col-md-2 fixed-height mb-5 selectable"
            :title="v.name"
            v-for="v in profileVaults"
            :key="v.id"
            @click="goToVault(v.id)"
          >
            <img :src="v.img" alt="" class="img-fluid fixed-height" />
            {{ v.name }}
          </div>
        </div>
      </div>
    </div>
    <div class="row ms-5">
      <div class="col-12">
        <h3>Keeps</h3>
      </div>
    </div>
    <div class="masonry-with-columns ms-5 mb-5 me-5">
      <div v-for="k in profileKeeps" :key="k.id" class="">
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { useRoute, useRouter } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
export default {
  setup() {
    const route = useRoute()
    const router = useRouter()
    onMounted(async () => {
      try {
        await profilesService.getProfile(route.params.id)
        await profilesService.getProfileVaults(route.params.id)
        await profilesService.getProfileKeeps(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      profile: computed(() => AppState.activeProfile),
      profileVaults: computed(() => AppState.activeProfileVaults),
      profileKeeps: computed(() => AppState.activeProfileKeeps),
      async goToVault(id) {
        await vaultsService.getVault(id)
        router.push({ name: 'Vault', params: { id } })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.fixed-height {
  height: 20vh;
  width: 20vh;
  object-fit: cover;
}
.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    display: inline-block;
    width: 100%;
  }
}
</style>
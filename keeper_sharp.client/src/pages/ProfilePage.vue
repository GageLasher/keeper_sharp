<template>
  <div class="container-fluid m-0">
    <div class="row m-0">
      <div class="col-12 d-flex mt-5">
        <!-- <div class="row"> -->
        <!-- <div class="col-md-6 m-5 d-flex"> -->
        <img :src="profile?.picture" alt="" class="img-fluid rounded" />
        <div class="row ms-3">
          <h2 class="">{{ profile.name }}</h2>
          <h5 v-if="account.id != profile.id">
            Vaults: {{ profileVaults.length }}
          </h5>
          <h5 v-if="account.id == profile.id">
            Vaults: {{ accountVaults.length }}
          </h5>
          <h5>Keeps: {{ profileKeeps.length }}</h5>
          <!-- </div> -->
          <!-- </div> -->
        </div>
      </div>
    </div>
    <div class="row m-5">
      <div class="col-12 d-flex">
        <h3>Vaults</h3>
        <h3
          v-if="profile.id == account.id"
          class="bg-green ms-2 selectable"
          title="create vault"
          data-bs-toggle="modal"
          data-bs-target="#create-vault"
        >
          +
        </h3>
      </div>

      <div class="col-12">
        <div class="row container p-0" v-if="account.id != profile.id">
          <div
            class="col-md-3 fixed-height mb-5 selectable"
            :title="v.name"
            v-for="v in profileVaults"
            :key="v.id"
            @click="goToVault(v.id)"
          >
            <img
              :src="v.img"
              alt=""
              class="img-fluid fixed-height rounded cover-fit"
            />
            <h6 class="bottom-left">
              {{ v.name }}
            </h6>
          </div>
        </div>
        <div class="row container p-0" v-if="account.id == profile.id">
          <div
            class="col-md-3 fixed-height mb-5 selectable"
            :title="v.name"
            v-for="v in accountVaults"
            :key="v.id"
            @click="goToVault(v.id)"
          >
            <img
              :src="v.img"
              alt=""
              class="img-fluid fixed-height rounded cover-fit"
            />
            <h6 class="bottom-left">
              {{ v.name }}
            </h6>
          </div>
        </div>
      </div>
    </div>
    <div class="row ms-5">
      <div class="col-12 d-flex">
        <h3>Keeps</h3>
        <h3
          v-if="profile.id == account.id"
          class="bg-green ms-2 selectable"
          title="create keep"
          data-bs-toggle="modal"
          data-bs-target="#create-keep"
        >
          +
        </h3>
      </div>
    </div>
    <div class="masonry-with-columns ms-5 mb-5 me-5">
      <div v-for="k in profileKeeps" :key="k.id" class="">
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>
  <CreateKeepModal2 />
  <CreateVaultModal2 />
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
      account: computed(() => AppState.account),
      accountVaults: computed(() => AppState.vaults),
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
.bg-green {
  color: rgb(23, 190, 23);
}
.container {
  position: relative;

  color: white;

  text-shadow: 2px 2px #2c2a2a;
}
.cover-fit {
  object-fit: cover;
}
.bottom-left {
  position: absolute;
  bottom: 8px;
  left: 16px;
}
h6 {
  font-size: 10pt;
  font-weight: bold;
}
</style>
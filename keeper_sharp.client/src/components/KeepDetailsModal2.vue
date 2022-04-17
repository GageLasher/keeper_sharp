<template>
  <Modal id="active-keep">
    <template #modal-body>
      <div class="container">
        <div class="row">
          <div class="col-md-6 p-2">
            <img
              height="500"
              class="w-100 object-fit-cover"
              :src="keep.img"
              alt=""
            />
          </div>
          <div class="col-md-6 p-2">
            <div class="row">
              <div class="col-12 d-flex justify-content-end">
                <button
                  type="button"
                  class="btn-close"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                ></button>
              </div>
            </div>
            <div class="row justify-content-center mt-2">
              <div class="col-4 d-flex justify-content-around">
                <h5>
                  <i class="mdi mdi-eye">
                    <span class="ms-2"> {{ keep.views }} </span>
                  </i>
                </h5>
                <h5>
                  <i class="mdi mdi-file-key">
                    <span class="ms-2"> {{ keep.kept }} </span>
                  </i>
                </h5>
              </div>
            </div>
            <div class="row justify-content-center">
              <div class="col-12 text-center mt-4">
                <h2>
                  {{ keep.name }}
                </h2>
                <p class="mt-3">
                  {{ keep.description }}
                </p>
              </div>
              <div class="col-8 border-bottom mt-5"></div>
            </div>
            <div class="spacer"></div>
            <div class="row mt-5 align-items-center">
              <div class="col-12 d-flex justify-content-between">
                <div class="dropdown">
                  <button
                    class="btn btn-outline-success dropdown-toggle"
                    type="button"
                    id="dropdownMenuButton1"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                  >
                    ADD TO VAULT
                  </button>
                  <ul
                    class="dropdown-menu"
                    aria-labelledby="dropdownMenuButton1"
                  >
                    <li
                      v-for="mV in myVaults"
                      :key="mV.id"
                      @click="addToVault(mV.id)"
                    >
                      <a class="dropdown-item" href="#">{{ mV.name }} </a>
                    </li>
                  </ul>
                </div>
                <h5
                  v-if="user?.id == keep.creator?.id"
                  @click="removeKeep(keep.vaultKeepId, keep.id)"
                >
                  <i class="mdi mdi-delete selectable" title="delete keep"></i>
                </h5>
                <div class="div d-flex">
                  <img
                    :src="keep.creator?.picture"
                    alt=""
                    class="img-fluid pp rounded selectable"
                    :title="keep.creator?.name"
                    @click="goToProfile(keep.creator?.id)"
                  />
                  <p class="clip-text ms-2">{{ keep.creator?.name }}</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState.js'
import { Modal } from 'bootstrap'
import { useRoute, useRouter } from 'vue-router'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
import { vaultsService } from '../services/VaultsService.js'
import { watchEffect } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService.js'
export default {
  setup() {
    const router = useRouter()
    const route = useRoute()
    // watchEffect(async () => {
    //   try {
    //     await vaultsService.getAll()
    //   } catch (error) {
    //     logger.log(error)
    //     Pop.toast(error.message)
    //   }
    // })
    return {
      myVaults: computed(() => AppState.vaults.filter(v => v.creator.id == AppState.account.id)),
      user: computed(() => AppState.account),
      keep: computed(() => AppState.activeKeep),
      goToProfile(id) {
        Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
        router.push({ name: 'Profile', params: { id } })
      },
      async addToVault(id) {
        try {
          let body = { keepId: AppState.activeKeep.id, vaultId: id }
          await vaultsService.addToVault(body)
          AppState.activeKeep.kept++
          Pop.toast("Added keep to your vault", "success")
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      async removeKeep(id, keepId) {
        try {

          if (route.name == "Vault") {

            if (await Pop.confirm("You sure you want to delete this?")) {


              await keepsService.removeVaultKeep(id)
              Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
              await vaultsService.getVaultKeeps(AppState.activeVault.id)

            }
          }

          if (route.name != "Vault") {

            if (await Pop.confirm("You sure you want to delete this?")) {


              await keepsService.removeKeep(id, keepId)
              Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
              await keepsService.getAll()

            }
          }




        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.border-bottom {
  color: black;
}
.spacer {
  height: 22vh;
}
.pp {
  height: 3vh;
  width: 3vh;
}
</style>
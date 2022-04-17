<template>
  <Modal id="create-keep">
    <template #modal-body>
      <form @submit.prevent="createKeep">
        <div class="row d-flex justify-content-center m-5">
          <div class="col-12 d-flex justify-content-between">
            <h2>New Keep</h2>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="col-12 p-3">
            <label for="">Name: </label>
            <input
              v-model="editable.name"
              type="text"
              name=""
              id=""
              class="ms-2"
              required
            />
          </div>
          <div class="col-12 p-3">
            <label for="">Image Url: </label>
            <input
              v-model="editable.img"
              type="text"
              name=""
              id=""
              class="ms-2"
              required
            />
          </div>
          <div class="col-12 p-3">
            <label for="">Description: </label>
            <input
              v-model="editable.description"
              type="text"
              name=""
              id=""
              class="ms-2"
              required
            />
          </div>

          <div class="col-12">
            <button class="btn btn-primary" type="submit">Submit</button>
          </div>
        </div>
      </form>
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
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {

          await keepsService.create(editable.value)
          Modal.getOrCreateInstance(document.getElementById('create-keep')).hide()
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
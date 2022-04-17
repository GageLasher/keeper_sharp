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
                <button class="btn btn-outline-success">ADD TO VAULT</button>
                <h5 v-if="user?.id == keep.creator?.id">
                  <i class="mdi mdi-delete" title="delete keep"></i>
                </h5>
                <div class="div d-flex">
                  <img
                    :src="keep.creator?.picture"
                    alt=""
                    class="img-fluid pp rounded"
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
import { useRouter } from 'vue-router'
export default {
  setup() {
    const router = useRouter()
    const review = ref({})
    return {
      review,
      user: computed(() => AppState.account),
      keep: computed(() => AppState.activeKeep),
      goToProfile(id) {
        Modal.getOrCreateInstance(document.getElementById('active-restaurant')).hide()
        router.push({ name: 'Profile', params: { id } })
      },
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
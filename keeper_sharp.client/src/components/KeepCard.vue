<template>
  <div
    class="project m-3 container rounded shadow p-0"
    @click="setActive"
    data-bs-toggle="modal"
    data-bs-target="#active-keep"
  >
    <img :src="keep.img" alt="" class="img-fluid rounded" />
    <div class="row bottom-right">
      <div class="col-12 d-flex justify-content-between align-items-center">
        <h5 class="p-1">{{ keep.name }}</h5>
        <img :src="keep.creator.picture" alt="" class="img-fluid pp p-1" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { keepsService } from '../services/KeepsService'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      coverImg: computed(() => `url('${props.keep.img}')`),
      async setActive() {
        await keepsService.setActive(props.keep)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.cover-img {
  background-image: v-bind(coverImg);
  background-size: cover;
  min-height: 100%;
  background-position: center;
}
.project {
  min-height: 100%;
  transition: all 0.12s ease;
  box-shadow: 0px 5px 5px rgba(0, 0, 0, 0.3);
}
.project:hover {
  transform: scale(1.05);
  outline: 1px solid var(--bs-light);
  box-shadow: 0px 15px 10px rgba(0, 0, 0, 0.3);
}
.pp {
  border-radius: 50%;
  width: 5vh;
  height: 5vh;
}
.container {
  position: relative;

  color: white;

  background: linear-gradient(to top, rgba(0, 0, 0, 0.85), transparent);
  text-shadow: 2px 2px #2c2a2a;
}

.bottom-right {
  position: absolute;
  bottom: 8px;
  right: 16px;
}
h5 {
  font-weight: bold;
}
</style>
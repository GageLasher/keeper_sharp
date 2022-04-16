<template>
  <div class="cover-img m-3">
    <div class="p-2 bg-dark-blur rounded text-center text-light">
      {{ keep.name }}
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      coverImg: computed(() => `url('${props.keep.img}')`)
    }
  }
}
</script>


<style lang="scss" scoped>
.cover-img {
  background-image: v-bind(coverImg);
  background-size: cover;
  background-position: center;
}
</style>
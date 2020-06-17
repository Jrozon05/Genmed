import Vue from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import axios from 'axios'

import Vuelidate from 'vuelidate'

import CoreuiVue from '@coreui/vue'
import { iconsSet as icons } from './assets/icons/icons.js'

axios.defaults.baseURL = 'http://localhost:5000/api'

Vue.use(CoreuiVue)
Vue.use(Vuelidate)
Vue.config.productionTip = false

new Vue({
  router,
  store,
  icons,
  render: h => h(App)
}).$mount('#app')

import Vue from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import axios from 'axios'

import Vuelidate from 'vuelidate'
import VueFormWizard from 'vue-form-wizard'
import Editor from '@morioh/v-quill-editor'

import 'vue-form-wizard/dist/vue-form-wizard.min.css'
import '@morioh/v-quill-editor/dist/editor.css'
import CoreuiVue from '@coreui/vue'
import { iconsSet as icons } from './assets/icons/icons.js'
import VueAlertify from 'vue-alertify'

axios.defaults.baseURL = 'http://localhost:5000/api'
// axios.defaults.baseURL = 'https://server.genmedrd.com/api'
// axios.defaults.baseURL = 'https://devserver.genmedrd.com/api'

Vue.use(CoreuiVue)
Vue.use(Vuelidate)
Vue.use(VueFormWizard)
Vue.use(Editor)
Vue.use(VueAlertify, {
  notifier: {
    delay: 5,
    position: 'top-right',
    closeButton: true
  }
})
Vue.config.productionTip = false

new Vue({
  router,
  store,
  icons,
  render: h => h(App)
}).$mount('#app')

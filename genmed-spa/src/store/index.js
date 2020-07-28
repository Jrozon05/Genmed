import Vue from 'vue'
import Vuex from 'vuex'

import { auth } from './modules/auth'
import { sidebar } from './modules/sidebar'
import { usuario } from './modules/usuario'

Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    auth,
    sidebar,
    usuario
  }
})

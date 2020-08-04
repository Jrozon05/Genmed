import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../views/auth/Login.vue'

const TheContainer = () => import('@/containers/TheContainer')
const Dashboard = () => import('@/views/Dashboard')
const Usuario = () => import('@/views/usuario/Usuario')
const UpdateUsuario = () => import('@/views/usuario/UpdateUsuario')
const Paciente = () => import('@/views/paciente/Pacientes')
const NuevoPaciente = () => import('@/views/paciente/CreatePaciente')
const Posicion = () => import('@/views/posicion/Posicion')

Vue.use(VueRouter)

const routes = [{
    path: '/login',
    redirect: '/',
    component: Login
  },
  {
    path: '/',
    name: 'Login',
    component: Login
  },
  {
    path: '/home',
    redirect: '/dashboard',
    name: 'Home',
    component: TheContainer,
    children: [{
        path: '/dashboard',
        name: 'Dashboard',
        component: Dashboard
      },
      {
        path: '/paciente',
        name: 'Paciente',
        component: Paciente
      },
      {
        path: '/nuevopaciente',
        name: 'Paciente / Create Paciente',
        component: NuevoPaciente
      },
      {
        path: '/usuario',
        name: 'Usuario',
        component: Usuario
      },
      {
        path: '/usuario/:guid',
        name: 'Usuario / Modificar Usuario',
        component: UpdateUsuario
      },
      {
        path: '/posicion',
        name: 'Posiciones',
        component: Posicion
      }
    ]
  },
  {
    path: '*',
    redirect: '/'
  }
]

const router = new VueRouter({
  mode: 'history',
  linkActiveClass: 'active',
  scrollBehavior: () => ({
    y: 0
  }),
  base: process.env.BASE_URL,
  routes
})

router.beforeEach((to, from, next) => {
  const publicPages = ['/login', '/']
  const authRequired = !publicPages.includes(to.path)
  const loggedIn = localStorage.getItem('usuario')

  // trying to access a restricted page + not logged in
  // redirect to login page
  if (authRequired && !loggedIn) {
    next('/login')
  } else {
    next()
  }
})

export default router

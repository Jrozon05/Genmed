<template>
  <div class="d-flex align-items-center min-vh-100">
    <CContainer fluid>
      <CRow class="justify-content-center">
        <CCol md="4">
          <CCard class="mx-4 mb-0">
            <CCardBody class="p-4">
              <CForm>
                <h1>Login</h1>
                <p class="text-muted">Iniciar sesión en su cuenta</p>
                <CAlert color="danger" closeButton :show.sync="alert" class="alert-dismissible" v-if="message">
                  {{ message.error }}
                </CAlert>
                <CInput placeholder="Nombre Usuario"
                  autocomplete="nombreusuario"
                  v-model="usuario.nombreUsuario"
                  @input="$v.usuario.nombreUsuario.$touch()"
                  :is-valid="!$v.usuario.nombreUsuario.$error ? null : false"
                  invalid-feedback="El nombre usuario es un campo requerido">
                  <template #prepend-content>
                    <CIcon name="cil-user" />
                  </template>
                  {{$v.usuario.nombreUsuario}}
                </CInput>
                <CInput placeholder="Contraseña" type="password"
                  autocomplete="curent-password"
                  v-model="usuario.clave"
                  :is-valid="!$v.usuario.clave.$error ? null : false"
                  invalid-feedback="La clave es un campo requerido">
                  <template #prepend-content>
                    <CIcon name="cil-lock-locked" />
                  </template>
                </CInput>
                <CRow>
                  <CCol col="6" class="text-left">
                    <CButton color="primary" class="px-4" @click.prevent="Login">Login</CButton>
                  </CCol>
                  <CCol col="6" class="text-right">
                    <CButton color="link" class="px-0">Se te olvidó tu contraseña?</CButton>
                  </CCol>
                </CRow>
              </CForm>
              <br>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
    </CContainer>
  </div>
</template>

<script>
import Usuario from '../../models/usuario'
import { required, minLength } from 'vuelidate/lib/validators'

export default {
  name: 'Login',
  data () {
    return {
      usuario: new Usuario('', ''),
      submitted: false,
      message: '',
      alert: false
    }
  },
  validations: {
    usuario: {
      nombreUsuario: {
        required
      },
      clave: {
        required,
        minLength: minLength(5)
      }
    }
  },
  methods: {
    Login () {
      this.$v.$touch()
      if (this.$v.$invalid) {
          return
      }
      if (this.usuario.nombreUsuario && this.usuario.clave) {
        this.$store.dispatch('auth/login', this.usuario).then(() => {
           this.$router.push('/dashboard')
          }, error => {
            this.alert = true
            this.message = (error.response && error.response.data) || error.message || error.toString()
          })
      }
    }
  }
}
</script>

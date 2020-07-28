<template>
    <div>
        <CRow>
            <CCol sm="4">
                <CCard>
                    <CCardHeader>
                        <strong>Detalle Usuario </strong>
                    </CCardHeader>
                    <CCardBody>
                        <CRow>
                        <CCol sm="12">
                            <picture-input
                                ref="pictureInput"
                                width="200"
                                height="150"
                                margin="16"
                                accept="image/jpeg,image/png"
                                size="10"
                                :removable="true"
                                :customStrings="{
                                    upload: '<h1>Bummer!</h1>',
                                    drag: 'Arrastra o busca una 😺'
                                }">
                            </picture-input>
                            <br />
                            <div style="text-align: center;">
                                <h2>{{usuario.nombreCompleto}}</h2>
                                <h5>{{usuario.posicion.toUpperCase()}}</h5>
                            </div>
                            <hr/>
                        </CCol>
                        </CRow>
                        <CRow>
                        <CCol sm="12">
                            <CInput
                            label="Identificación:"
                            placeholder="Introduzca su nombre de usuario"
                            disabled
                            v-model="usuario.guid"
                            />
                        </CCol>
                        </CRow>
                    </CCardBody>
                </CCard>
            </CCol>
                        <CCol sm="8">
                <CCard>
                    <CCardHeader>
                        <strong>Editar Usuario </strong>
                    </CCardHeader>
                    <CCardBody>
                        <CRow>
                            <CCol sm="12">
                                <CAlert color="danger" closeButton :show.sync="alert" class="alert-dismissible" v-if="message">
                                    {{ message }}
                                </CAlert>
                            </CCol>
                        </CRow>
                        <CRow>
                        <CCol sm="12">
                            <CInput
                            label="Nombre Usuario"
                            placeholder="Introduzca su nombre de usuario"
                            disabled
                            v-model="usuario.nombreUsuario"
                            />
                        </CCol>
                        </CRow>
                        <CRow>
                        <CCol sm="12">
                            <CInput
                            label="Contraseña"
                            placeholder="Introduzca su constraseña"
                            type="password"
                            v-model="usuario.clave"
                            />
                        </CCol>
                        </CRow>
                        <CRow>
                        <CCol sm="12">
                            <CInput
                            label="Confirmar Contraseña"
                            placeholder="Confirmar su constraseña"
                            type="password"
                            v-model="usuario.confirmarClave"
                            />
                        </CCol>
                        </CRow>
                        <CRow>
                        <CCol sm="12">
                            <div class="form-group">
                                <label for="">Doctores</label>
                                <select class="form-control" v-model="usuario.doctorId">
                                    <option v-for="doctor in doctores" :key="doctor.id" :value="doctor.doctorId">{{doctor.nombreCompleto}}</option>
                                </select>
                            </div>
                        </CCol>
                        </CRow>
                        <CRow>
                        <CCol sm="12">
                            <div class="form-group">
                                <label for="">Roles</label>
                                <select class="form-control" v-model="usuario.rolId">
                                    <option value="1">Super Admin</option>
                                    <option value="2">Admin</option>
                                    <option value="3">Usuario</option>
                                    <option value="4">Solo Lectura</option>
                                </select>
                            </div>
                        </CCol>
                        </CRow>
                    </CCardBody>
                        <CCardFooter>
                            <CButton type="submit" color="success" @click.prevent="UpdateUsuario">Salvar</CButton>&nbsp;
                            <CButton type="submit" variant="outline" color="dark" @click.prevent="CancelarForm">Cancelar</CButton>
                            <CButton type="submit" color="danger" @click.prevent="DeleteUsuario" class="btnFloat">Borrar</CButton>
                    </CCardFooter>
                </CCard>
            </CCol>
        </CRow>
    </div>
</template>

<script>
import UsuarioService from '../../services/usuario-service'
import DoctorService from '../../services/doctor-service'
import PictureInput from 'vue-picture-input'

const fields = [
    { key: 'nombreCompleto', label: 'Doctor', _style: 'width:15%' },
    { key: 'nombreUsuario', _style: 'width:20%' },
    { key: 'guid', _style: 'width:30%' },
    { key: 'posicion', label: 'Posición' },
    { key: 'rol' },
    { key: 'editar', label: 'Editar', _style: 'width:1%', sorter: false, filter: false }
]

export default {
  name: 'Table',
  data () {
      return {
          fields,
          usuarios: [],
          doctores: [],
          usuario: {
              guid: '',
              nombreUsuario: '',
              clave: '',
              confirmarClave: '',
              doctorId: 0,
              rolId: '',
              posicion: '',
              nombreCompleto: ''
          },
          message: '',
          alert: false
      }
  },
  components: {
    PictureInput
  },
  computed: {
      guidToEdit () {
         return this.$store.getters['usuario/getGuidToEdit']
      }
  },
  mounted () {
      this.getDoctores()
      this.changeUsuarioInfo(this.guidToEdit)
  },
  watch: {
      guidToEdit () {
          this.changeUsuarioInfo(this.guidToEdit)
      }
  },
  methods: {
    getDoctores () {
        DoctorService.getDoctores().then(
            response => {
                const data = response.data
                for (const key in data) {
                    const usuario = data[key]
                    usuario.id = key
                    this.doctores.push(usuario)
                }
            }
        )
    },
    changeUsuarioInfo (item) {
        UsuarioService.getUsuarioByGuid(item).then(
            response => {
                const data = response.data
                const usuario = data
                this.usuario.nombreUsuario = usuario.nombreUsuario
                this.usuario.doctorId = usuario.doctor[0].doctorId
                this.usuario.rolId = usuario.rol.rolId
                this.usuario.posicion = usuario.doctor[0].posicion
                this.usuario.nombreCompleto = usuario.doctor[0].nombreCompleto
                this.usuario.guid = usuario.guid
            }
        )
    },
    onChanged () {
        console.log('New picture loaded')
    },
    onRemoved () {
        this.image = ''
    },
    CancelarForm () {
        this.$router.push('/usuario')
    }
  }
}
</script>

<style scoped>
    .btnFloat {
        float: right;
    }
</style>

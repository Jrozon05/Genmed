<template>
    <div>
        <CRow>
            <CCol sm="6">
            <CRow>
                <CCol sm="12">
                    <CAlert color="dark">
                        <strong>EMBARAZO ACTUAL (SI APLICA)</strong>
                    </CAlert>
                </CCol>
            </CRow>
            <CRow>
                <CCol sm="6">
                    <CInput
                    label="Fecha de última Menstruación"
                    type="date"
                    />
                </CCol>
                <CCol sm="6">
                    <CInput
                    label="Fecha de última Papanicolaou"
                    type="date"
                    />
                </CCol>
            </CRow>
            <CRow>
                <CCol sm="4">
                    <CInput
                    label="Semanas"
                    type="number"
                    placeholder=""
                    />
                </CCol>
                <CCol sm="4">
                <CInput
                label="Peso (Lbs)"
                type="number"
                placeholder=""
                />
            </CCol>
            <CCol sm="4">
                <CInput
                label="Estatura (Pies)"
                type="number"
                placeholder=""
                />
            </CCol>
            </CRow>
            <CRow>
                <CCol sm="4">
                    <CInput
                    label="Fecha"
                    type="date"
                    />
                </CCol>
                <CCol sm="4">
                    <CInput
                    label="Lugar"
                    placeholder=""
                    />
                </CCol>
                <CCol sm="4">
                    <CInput
                    label="Motivo"
                    placeholder=""
                    />
                </CCol>
            </CRow>
            </CCol>
                <CCol sm="6">
                    <CRow>
                <CCol sm="12">
                    <CAlert color="dark">
                        <strong>EXAMENES GENERAL</strong>
                    </CAlert>
                </CCol>
            </CRow>
            <CRow>
                    <CCol sm="12" md="12">
                        <CRow form class="form-group">
                            <CCol sm="4">
                                Clínico
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                            <CCol sm="4">
                                Mama
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                        </CRow>
                        <CRow form class="form-group">
                            <CCol sm="4">
                                Odontológico
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                            <CCol sm="4">
                                Colposcopia
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                        </CRow>
                        <CRow form class="form-group">
                            <CCol sm="4">
                                Papanicolaou
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                            <CCol sm="4">
                                Pervical
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                        </CRow>
                        <CRow form class="form-group">
                            <CCol sm="4">
                                Cervical
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                        </CRow>
                    </CCol>
                </CRow>
            </CCol>
        </CRow>
        <hr>
    </div>
</template>

<script>

  export default {
    name: 'name',
    data () {
      return {
          paciente: {
              phone: '',
              phoneAlternativo: ''
          },
          ciudades: [
            {
                countryCode: 'CA',
                countryName: 'Canada'
            }
        ]
      }
    }
  }
</script>

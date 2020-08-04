<template>
    <div>
        <CAlert color="dark">
            <strong>ANTECEDENTES PERSONALES</strong>
        </CAlert>
        <CRow>
            <CCol sm="5">
                <CRow>
                    <CCol sm="12">
                        <CAlert color="info">
                            <strong>Patolólogicos Personales</strong>
                        </CAlert>
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="12" md="12">
                        <CRow form class="form-group">
                            <CCol sm="4">
                                Diabetes
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                            <CCol sm="4">
                                Hipertensión
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                        </CRow>
                        <CRow form class="form-group">
                            <CCol sm="4">
                                Cáncer
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                            <CCol sm="4">
                                Cardiopatías
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                        </CRow>
                        <CRow form class="form-group">
                            <CCol sm="4">
                                T.B Pulmonar
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                            <CCol sm="4">
                                Nefropatías
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                        </CRow>
                        <CRow form class="form-group">
                            <CCol sm="4">
                                Hepatitis
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                            <CCol sm="4">
                                ETS's
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                        </CRow>
                        <CRow form class="form-group">
                            <CCol sm="4">
                                Infertilidad
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                            <CCol sm="4">
                                Cirugía Pélvico-Uterino
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                            </CCol>
                        </CRow>
                        <hr />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="12" md="12">
                        <CAlert color="info">
                            <strong>No Patolólogicos</strong>
                        </CAlert>
                        <CRow>
                            <CCol xs="12" md="12">
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Tabaco
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Alcohol
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Café
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Alérgicos
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Traumáticos
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Transusionales
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Quirúrgicos
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                            </CCol>
                        </CRow>
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="12" md="12">
                        <hr />
                        <CAlert color="info">
                            <strong>Patolólogicos Familiares</strong>
                        </CAlert>
                        <CRow>
                            <CCol xs="12" md="12">
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Diabetes
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Hipertensión
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Cáncer
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Cardiopatías
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        T.B Pulmonar
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Hemopatías
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Neuropatías
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Psicopatías
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        M. Congenitas
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Embarazo Múltiples
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                            </CCol>
                        </CRow>
                    </CCol>
                </CRow>
            </CCol>
            <CCol sm="7">
                <CRow>
                    <CCol sm="12">
                    <CAlert color="info">
                        <strong>Gineco-Obstétrico</strong>
                    </CAlert>
                </CCol>
                </CRow>
                <CRow>
                    <CCol sm="6">
                        <CInput
                        label="Menarquía (Años)"
                        type="number"
                        placeholder="Introduzca menarquía"
                        />
                    </CCol>
                </CRow>
                <CRow class="form-group py-1">
                    <CCol sm="2">
                        Flujo Vaginal <CSwitch class="mx-1 py-2" color="primary" size="sm" v-bind="labelIcon"/>
                    </CCol>
                    <CCol sm="2">
                        Dolor Pélvico <CSwitch class="mx-1 py-2" color="primary" size="sm" v-bind="labelIcon"/>
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="6">
                        <CInput
                        label="Intervalo del Flujo Menstrual (Días)"
                        type="number"
                        placeholder="Introduzca Intervalo"
                        />
                    </CCol>
                    <CCol sm="6">
                        <CInput
                        label="Duración del Flujo Menstrual (Días)"
                        type="number"
                        placeholder="Introduzca Duración"
                        />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="6">
                        <CInput
                        label="Telarquia (Años)"
                        type="number"
                        placeholder="Introduzca Telarquia"
                        />
                    </CCol>
                    <CCol sm="6">
                        <CInput
                        label="Pubarquia (Años)"
                        type="number"
                        placeholder="Introduzca Pubarquia"
                        />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="6">
                        <CInput
                        label="Vida Sexual Activa (Años)"
                        type="number"
                        placeholder="Dejar en blanco si no aplica."
                        />
                    </CCol>
                    <CCol sm="6">
                        <CInput
                        label="Número de Cónyuge"
                        type="number"
                        placeholder="Introduzca la cantidad de cónyuge"
                        />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="6">
                        <div class="form-group">
                            <label for="">Método para el Control Natal</label>
                            <select class="form-control">
                                <option value="0">Seleccionar el método natal</option>
                            </select>
                        </div>
                    </CCol>
                    <CCol sm="6">
                        <CInput
                        label="Fecha del último Papanicolaou"
                        type="date"
                        />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="4">
                        <CInput
                        label="Gesta"
                        type="number"
                        placeholder=""
                        />
                    </CCol>
                    <CCol sm="4">
                        <CInput
                        label="Parto"
                        type="number"
                        placeholder=""
                        />
                    </CCol>
                    <CCol sm="4">
                        <CInput
                        label="Cesárea"
                        type="number"
                        placeholder=""
                        />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="4">
                        <CInput
                        label="Aborto"
                        type="number"
                        placeholder=""
                        />
                    </CCol>
                    <CCol sm="4">
                        <CInput
                        label="Legrado"
                        type="number"
                        placeholder=""
                        />
                    </CCol>
                    <CCol sm="4">
                        <CInput
                        label="Hijos Vivos"
                        type="number"
                        placeholder=""
                        />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="4">
                        <CInput
                        label="Hijos Muertos"
                        type="number"
                        placeholder=""
                        />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="12" md="12">
                        <CRow>
                            <CCol xs="12" md="12">
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Sangrado Vaginal Anormal
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Hemorragia después del coito
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Hemorragia Posmenopáusico
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Masa Pélvica
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Masa Mamaria
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Dolor Mamario
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Secresión en el Pezón
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1" color="primary" size="sm" v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
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
      name: 'HistorialClinico',
      data () {
          return {
            labelIcon: {
                labelOn: '\u2713',
                labelOff: '\u2715'
            }
          }
      }
  }
</script>

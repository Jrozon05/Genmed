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
                                <CSwitch class="mx-1 py-1"
                                color="primary"
                                size="sm"
                                :checked.sync="historial.diabetes"
                                v-model="historial.diabetes"
                                v-bind="labelIcon"/>
                            </CCol>
                            <CCol sm="4">
                                Hipertensión
                            </CCol>
                            <CCol sm="2">
                                <CSwitch
                                class="mx-1 py-1"
                                color="primary"
                                size="sm"
                                :checked.sync="historial.hipertension"
                                v-model="historial.hipertension"
                                v-bind="labelIcon"/>
                            </CCol>
                        </CRow>
                        <CRow form class="form-group">
                            <CCol sm="4">
                                Cáncer
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1
                                py-1"
                                color="primary"
                                size="sm"
                                :checked.sync="historial.cancer"
                                v-model="historial.cancer"
                                v-bind="labelIcon"/>
                            </CCol>
                            <CCol sm="4">
                                Cardiopatías
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1"
                                color="primary"
                                size="sm"
                                :checked.sync="historial.cardiopatias"
                                v-model="historial.cardiopatias"
                                v-bind="labelIcon"/>
                            </CCol>
                        </CRow>
                        <CRow form class="form-group">
                            <CCol sm="4">
                                T.B Pulmonar
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1"
                                color="primary"
                                size="sm"
                                :checked.sync="historial.tbpulmonar"
                                v-model="historial.tbpulmonar"
                                v-bind="labelIcon"/>
                            </CCol>
                            <CCol sm="4">
                                Nefropatías
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1"
                                color="primary"
                                size="sm"
                                :checked.sync="historial.nefropatias"
                                v-model="historial.nefropatias"
                                v-bind="labelIcon"/>
                            </CCol>
                        </CRow>
                        <CRow form class="form-group">
                            <CCol sm="4">
                                Hepatitis
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1"
                                color="primary"
                                size="sm"
                                :checked.sync="historial.hepatitis"
                                v-model="historial.hepatitis"
                                v-bind="labelIcon"/>
                            </CCol>
                            <CCol sm="4">
                                ETS's
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1"
                                color="primary"
                                size="sm"
                                :checked.sync="historial.ets"
                                v-model="historial.ets"
                                v-bind="labelIcon"/>
                            </CCol>
                        </CRow>
                        <CRow form class="form-group">
                            <CCol sm="4">
                                Infertilidad
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1"
                                color="primary"
                                size="sm"
                                :checked.sync="historial.infertilidad"
                                v-model="historial.infertilidad"
                                v-bind="labelIcon"/>
                            </CCol>
                            <CCol sm="4">
                                Cirugía Pélvico-Uterino
                            </CCol>
                            <CCol sm="2">
                                <CSwitch class="mx-1 py-1"
                                color="primary"
                                size="sm"
                                :checked.sync="historial.pelvicouterino"
                                v-model="historial.pelvicouterino"
                                v-bind="labelIcon"/>
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
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.tabaco"
                                        v-model="historial.tabaco"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Alcohol
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.alcohol"
                                        v-model="historial.alcohol"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Café
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.cafe"
                                        v-model="historial.cafe"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Alérgicos
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.alergicos"
                                        v-model="historial.alergicos"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Traumáticos
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.traumaticos"
                                        v-model="historial.traumaticos"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Transusionales
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.transusionales"
                                        v-model="historial.transusionales"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Quirúrgicos
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.quirugicos"
                                        v-model="historial.quirugicos"
                                        v-bind="labelIcon"/>
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
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.diabetesFam"
                                        v-model="historial.diabetesFam"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Hipertensión
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.hipertensionFam"
                                        v-model="historial.hipertensionFam"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Cáncer
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.cancerFam"
                                        v-model="historial.cancerFam"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Cardiopatías
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.cardiopatiasFam"
                                        v-model="historial.cardiopatiasFam"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        T.B Pulmonar
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.tbpulmonarFam"
                                        v-model="historial.tbpulmonarFam"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Hemopatías
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.hemopatiaFam"
                                        v-model="historial.hemopatiaFam"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Neuropatías
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.neuropatiaFam"
                                        v-model="historial.neuropatiaFam"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Psicopatías
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.psicopatiaFam"
                                        v-model="historial.psicopatiaFam"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        M. Congenitas
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.congenitaFam"
                                        v-model="historial.congenitaFam"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Embarazo Múltiples
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.embarazoMultipleFam"
                                        v-model="historial.embarazoMultipleFam"
                                        v-bind="labelIcon"/>
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
                        v-model="historial.menarquia"
                        placeholder="Introduzca menarquía"
                        />
                    </CCol>
                </CRow>
                <CRow class="form-group py-1">
                    <CCol sm="2">
                        Flujo Vaginal <CSwitch
                        class="mx-1 py-2"
                        color="primary"
                        size="sm"
                        :checked.sync="historial.flujovaginal"
                        v-model="historial.flujovaginal"
                        v-bind="labelIcon"/>
                    </CCol>
                    <CCol sm="2">
                        Dolor Pélvico <CSwitch
                        class="mx-1 py-2"
                        color="primary"
                        size="sm"
                        :checked.sync="historial.dolorpelvico"
                        v-model="historial.dolorpelvico"
                        v-bind="labelIcon"/>
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="6">
                        <CInput
                        label="Intervalo del Flujo Menstrual (Días)"
                        type="number"
                        v-model="historial.intervaloFlujoMenstrual"
                        placeholder="Introduzca Intervalo"
                        />
                    </CCol>
                    <CCol sm="6">
                        <CInput
                        label="Duración del Flujo Menstrual (Días)"
                        type="number"
                        v-model="historial.duracionFlujoMenstrual"
                        placeholder="Introduzca Duración"
                        />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="6">
                        <CInput
                        label="Telarquia (Años)"
                        type="number"
                        v-model="historial.telarquia"
                        placeholder="Introduzca Telarquia"
                        />
                    </CCol>
                    <CCol sm="6">
                        <CInput
                        label="Pubarquia (Años)"
                        type="number"
                        v-model="historial.pubarquia"
                        placeholder="Introduzca Pubarquia"
                        />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="6">
                        <CInput
                        label="Vida Sexual Activa (Años)"
                        type="number"
                        v-model="historial.vidaSexualActiva"
                        placeholder="Dejar en blanco si no aplica."
                        />
                    </CCol>
                    <CCol sm="6">
                        <CInput
                        label="Número de Cónyuge"
                        type="number"
                        v-model="historial.numeroConyuge"
                        placeholder="Introduzca la cantidad de cónyuge"
                        />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="6">
                        <div class="form-group">
                            <label for="">Método para el Control Natal</label>
                            <select class="form-control" v-model="historial.metodoNatal">
                                <option value="0">Seleccionar el método natal</option>
                            </select>
                        </div>
                    </CCol>
                    <CCol sm="6">
                        <CInput
                        label="Fecha del último Papanicolaou"
                        v-model="historial.fuPapanicolaou"
                        type="date"
                        />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="4">
                        <CInput
                        label="Gesta"
                        type="number"
                        v-model="historial.noGesta"
                        placeholder=""
                        />
                    </CCol>
                    <CCol sm="4">
                        <CInput
                        label="Parto"
                        type="number"
                        v-model="historial.noParto"
                        placeholder=""
                        />
                    </CCol>
                    <CCol sm="4">
                        <CInput
                        label="Cesárea"
                        type="number"
                        v-model="historial.noCesarea"
                        placeholder=""
                        />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="4">
                        <CInput
                        label="Aborto"
                        type="number"
                        v-model="historial.noAborto"
                        placeholder=""
                        />
                    </CCol>
                    <CCol sm="4">
                        <CInput
                        label="Legrado"
                        type="number"
                        v-model="historial.noLegrado"
                        placeholder=""
                        />
                    </CCol>
                    <CCol sm="4">
                        <CInput
                        label="Hijos Vivos"
                        type="number"
                        v-model="historial.noHujosVivos"
                        placeholder=""
                        />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="4">
                        <CInput
                        label="Hijos Muertos"
                        type="number"
                        v-model="historial.noHijosMuertos"
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
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.sangradoVaginalAnormal"
                                        v-model="historial.sangradoVaginalAnormal"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Hemorragia después del coito
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.hemorragiaCoito"
                                        v-model="historial.hemorragiaCoito"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Hemorragia Posmenopáusico
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.hemorragiaPosmenopausico"
                                        v-model="historial.hemorragiaPosmenopausico"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Masa Pélvica
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.masaPelvica"
                                        v-model="historial.masaPelvica"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Masa Mamaria
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.masaMamaria"
                                        v-model="historial.masaMamaria"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                    <CCol sm="4">
                                        Dolor Mamario
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.dolorMamario"
                                        v-model="historial.dolorMamario"
                                        v-bind="labelIcon"/>
                                    </CCol>
                                </CRow>
                                <CRow form class="form-group">
                                    <CCol sm="4">
                                        Secresión en el Pezón
                                    </CCol>
                                    <CCol sm="2">
                                        <CSwitch class="mx-1 py-1"
                                        color="primary"
                                        size="sm"
                                        :checked.sync="historial.secresionPezon"
                                        v-model="historial.secresionPezon"
                                        v-bind="labelIcon"/>
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
      data () {
          return {
            historial: {
                diabetes: false,
                hipertension: false,
                cancer: false,
                cardiopatias: false,
                tbpulmonar: false,
                nefropatias: false,
                hepatitis: false,
                ets: false,
                infertilidad: false,
                pelvicouterino: false,
                tabaco: false,
                alcohol: false,
                cafe: false,
                alergicos: false,
                traumaticos: false,
                transusionales: false,
                quirugicos: false,
                diabetesFam: false,
                hipertensionFam: false,
                cancerFam: false,
                cardiopatiasFam: false,
                tbpulmonarFam: false,
                hemopatiaFam: false,
                neuropatiaFam: false,
                psicopatiaFam: false,
                congenitaFam: false,
                embarazoMultipleFam: false,
                menarquia: '',
                flujovaginal: false,
                dolorpelvico: false,
                intervaloFlujoMenstrual: 0,
                duracionFlujoMenstrual: 0,
                telarquia: 0,
                pubarquia: 0,
                vidaSexualActiva: 0,
                numeroConyuge: 0,
                metodoNatal: 0,
                fuPapanicolaou: '',
                noGesta: 0,
                noParto: 0,
                noCesarea: 0,
                noAborto: 0,
                noLegrado: 0,
                noHujosVivos: 0,
                noHijosMuertos: 0,
                sangradoVaginalAnormal: false,
                hemorragiaCoito: false,
                hemorragiaPosmenopausico: false,
                masaPelvica: false,
                masaMamaria: false,
                dolorMamario: false,
                secresionPezon: false
            },
            labelIcon: {
                labelOn: '\u2713',
                labelOff: '\u2715'
            }
          }
      },
      methods: {
       validate () {
            var isValid = true // !this.$v.$invalid
            if (isValid) {
                this.$store.dispatch('paciente/setHistoriaClinico', this.historial)
            }
            return isValid
        }
      }
  }
</script>

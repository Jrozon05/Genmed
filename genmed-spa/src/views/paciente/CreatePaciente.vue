<template>
    <div>
        <CRow>
            <CCol sm="12">
                <CCard>
                    <CCardHeader>
                        <strong>Registro de Paciente</strong>
                    </CCardHeader>
                    <CCardBody>
                        <form-wizard color="#636f83" title="" subtitle="">
                            <hr />
                            <tab-content title="Datos Generales" :before-change="() => validateStep('RegistroGenerales')">
                                <RegistroGenerales ref="RegistroGenerales"></RegistroGenerales>
                            </tab-content>
                            <tab-content title="Motivo de Consulta" :before-change="() => validateStep('MotivoConsulta')">
                                <MotivoConsulta ref="MotivoConsulta"></MotivoConsulta>
                            </tab-content>
                            <tab-content title="Historia Clinica">
                                <HistoriaClinico></HistoriaClinico>
                            </tab-content>
                            <tab-content title="Exploración Fisica">
                                <ExploracionFisica></ExploracionFisica>
                            </tab-content>
                            <tab-content title="Diagnostico(s)">
                                <Diagnostico></Diagnostico>
                            </tab-content>
                            <tab-content title="Indicaciones / Medicamentos">
                                <Indicaciones></Indicaciones>
                            </tab-content>
                            <CButton type="submit" variant="outline" color="primary" slot="prev">Atras</CButton>
                            <CButton type="submit" variant="outline" color="primary" slot="next">Siguiente</CButton>
                            <CButton type="submit" color="success" slot="finish">Completar</CButton>
                        </form-wizard>
                    </CCardBody>
                </CCard>
            </CCol>
        </CRow>
    </div>
</template>

<script>
  import RegistroGenerales from './RegistroGenerales'
  import MotivoConsulta from './MotivoConsulta'
  import HistoriaClinico from './HistoriaClinica'
  import ExploracionFisica from './ExploracionFisica'
  import Diagnostico from './Diagnostico'
  import Indicaciones from './Indicaciones'

  export default {
    name: 'name',
    components: {
      RegistroGenerales,
      MotivoConsulta,
      HistoriaClinico,
      ExploracionFisica,
      Diagnostico,
      Indicaciones
    },
    methods: {
        validateStep (name) {
            var refToValidate = this.$refs[name]
            return refToValidate.validate()
        }
        // ,
        // mergePartialModels (model, isValid) {
        //     if (isValid) {
        //     // merging each step model into the final model
        //         this.finalModel = Object.assign({}, this.finalModel, model)
        //     }
        // }
    }
  }
</script>

<style>
    .vue-form-wizard.md .wizard-navigation .wizard-progress-with-circle {
        position: relative;
        top: 30px;
        height: 4px;
    }

    .vue-form-wizard.md .wizard-icon-circle {
        width: 50px;
        height: 50px;
        font-size: 20px;
    }

    .vs__dropdown-toggle{
        height: calc(1.5em + 0.75rem + 2px);
    }

    .vs__search::placeholder {
        border: none;
        color: #768192;
    }
</style>

<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <div class="d-flex align-center">
        <v-img
          alt="Modulo Acadêmico"
          class="shrink mt-1 hidden-sm-and-down"
          contain
          min-width="100"
          src="logo.png"
          width="100"
        />
      </div>

      <v-spacer></v-spacer>
    </v-app-bar>

    <v-main>
      <h2>Modulo Acadêmico</h2>
      <Alunos />
    </v-main>
  </v-app>
</template>

<script>
import Alunos from "./components/Alunos";

export default {
  name: "App",

  components: {
    Alunos,
  },

  data: () => ({
    //
  }),
};
</script>

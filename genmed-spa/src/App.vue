<template>
  <div id="app">
    <img src="./assets/logo.png" />
    <h1>{{ msg }}</h1>
    <h2>Essential Links</h2>
    <ul>
      <li><a href="https://vuejs.org" target="_blank">Core Docs</a></li>
      <li><a href="https://forum.vuejs.org" target="_blank">Forum</a></li>
      <li>
        <a href="https://chat.vuejs.org" target="_blank">Community Chat</a>
      </li>
      <li><a href="https://twitter.com/vuejs" target="_blank">Twitter</a></li>
    </ul>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "app",
  data() {
    return {
      msg: "Welcome to Your Vue.js App",
      temperatures: []
    };
  },
  created() {
    axios
      .get("https://www.genmedrd.com/weatherforecast")
      .then(response => {
        console.log(response);
        const data = response.data;
        const temperatures = [];
        for (let key in data) {
          const temp = data[key];
          temp.id = key;
          this.temperatures.push(temp);
        }
      })
      .catch(error => console.log(error));
  }
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

h1,
h2 {
  font-weight: normal;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  display: inline-block;
  margin: 0 10px;
}

a {
  color: #42b983;
}
</style>

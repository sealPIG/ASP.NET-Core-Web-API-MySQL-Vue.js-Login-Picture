<template>
  <header class="p-3 bg-dark text-white">
    <div class="container">
      <div class="d-flex flex-wrap align-items-center justify-content-center justify-content-lg-start">
        <ul class="nav col-12 col-lg-auto me-lg-auto mb-2 justify-content-center mb-md-0">
          <li>
            <router-link to="/" class="nav-link px-2 text-white">Home</router-link>
          </li>
        </ul>

        <div class="text-end">
          <router-link to="/SignIn" class="btn btn-outline-light me-2">Sign In</router-link>
          <router-link to="/SignUp" class="btn btn-outline-light me-2">Sign Up</router-link>
        </div>
      </div>
    </div>
  </header>

  <router-view/>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

nav {
  padding: 30px;
}

nav a {
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: #5589cc;
}


h1 {
  padding-top:30px;
}
</style>

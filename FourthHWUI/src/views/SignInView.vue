<template>
  <main class="form-signin">
    <form @submit.prevent="submit">
      <h1 class="h3 mb-3 fw-normal">Sign In</h1>

      <h6>Email</h6>
      <div class="form-floating">
        <input type="email" class="form-control" name="email" placeholder="email" v-model="User.Email" />
        <label>Email</label>
      </div>

      <h6>Password</h6>
      <div class="form-floating">
        <input type="password" class="form-control" name="password" placeholder="Password" v-model="User.Password" />
        <label>Password</label>
      </div>

      <button class="w-100 btn btn-lg btn-primary" v-on:click="SignIn">
        Submit
      </button>
    </form>
  </main>
</template>

<script>
import axios from 'axios'
const URL = "http://localhost:5125/api/Staff/";

export default {
  data(){
      return{
          User:{
            Email:"",
            Password:"",
            Mode:0
          }
      }
  },
  methods:{
    CheckInvalidation(){
      console.log("enter");
      if(!this.User.Email){
        alert("Enter email first please.");
        return true;
      }
      if(!this.User.Password){
        alert("Enter password first please.");
        return true;
      }
      return false;
    },
    SignIn(){
      if(this.CheckInvalidation())
        return;

      axios.get(URL + this.User.Email + "/" + this.User.Password)
      .then((response)=>{
        this.User.Mode = response.data;
        console.log(this.User.Mode);
        switch(this.User.Mode){
            case 1:
                alert("使用者權限：新增");
                break;
            case 2:
                alert("使用者權限：刪除");
                break;
            case 3:
                alert("使用者權限：修改");
                break;
        }
        this.$router.push({ path: "/Sensor/" + this.User.Mode});
      })
      .catch((error) => { console.error(error) })
    },
  },
}
</script>
  
<style>
.form-signin {
  width: 100%;
  max-width: 330px;
  padding: 15px;
  margin: auto;
}

.form-signin .checkbox {
  font-weight: 400;
}

.form-signin .form-floating:focus-within {
  z-index: 2;
}

.form-signin input {
  border-radius: 10px;
  border-width: 2px;
}

.form-signin input[type="email"] {
  margin-bottom: -1px;
}

.form-signin input[type="password"] {
  margin-bottom: 10px;
}

h6 {
  padding-top:10px;
  text-align: left;
}
</style>
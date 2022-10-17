<template>
    <main class="form-signin">
        <form @submit.prevent="submit">
            <h1 class="h3 mb-3 fw-normal">Sign Up</h1>

            <h6>Name</h6>
            <div class="form-floating">
                <input type="text" class="form-control" name="name" placeholder="text" >
                <label>Name</label>
            </div>

            <h6>Email</h6>
            <div class="form-floating">
                <input type="email" class="form-control" name="email" placeholder="name@example.com">
                <label>Email</label>
            </div>

            <h6>Password</h6>
            <div class="form-floating">
                <input type="password" class="form-control" name="password" placeholder="Password">
                <label>Enter Password</label>
            </div>

            <h6>Repeat Password</h6>
            <div class="form-floating">
                <input type="password" class="form-control" name="password" placeholder="Password">
                <label>Repeat Password</label>
            </div>

            <h6>UserMode</h6>
            <div class="form-floating">
                <input type="password" class="form-control" name="password" placeholder="Password">
                <label>Repeat Password</label>
            </div>

            <button class="w-100 btn btn-lg btn-primary" type="submit" @click="UpdateClick()">Submit</button>
        </form>
    </main>
</template>

<script>
//import axios from 'axios'
//const URL = "http://localhost:5125/api/";

export default {
    data(){
    return{
        User:{
            Name:"",
            Email:"",
            Password:"",
            Mode:0
        },   
        UpdateClick(){

        },
    }
  },
}
</script>
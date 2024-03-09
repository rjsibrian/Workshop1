<template>
  <div class="home">
    <h1 class="titleApp">Welcome to Your App</h1>
    <p class="description">In this application, you can interact with the Microsoft Adventure Works Cycles database. There are three main modules where you can access and filter valuable information related to the company, enabling process improvement and gaining insights into the performance of the sales team.</p>

    <div class="icons-container">
      <router-link to="/persons">
        <div class="icon-container persons">
          <i class="material-icons icon">people</i>
          <span>Persons</span>
        </div>
      </router-link>

      <router-link to="/products">
        <div class="icon-container products">
          <i class="material-icons icon">shopping_bag</i>
          <span>Products</span>
        </div>
      </router-link>

      <router-link to="/sales">
        <div class="icon-container sales">
          <i class="material-icons icon">attach_money</i>
          <span>Sales</span>
        </div>
      </router-link>
    </div>
  </div>
</template>

<style>
.home {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  min-height: 100vh;
}

.titleApp {
  font-size: 3em;
  margin-bottom: 10px;
}

.description {
  font-size: 1.1em;
  margin: 40px;
}

.icons-container {
  display: flex;
  justify-content: space-around;
  width: 100%;
  max-width: 600px;
  margin-top: 20px;
}

.icon-container {
  text-align: center;
  cursor: pointer;
  border-radius: 10px;
  overflow: hidden;
  transition: background-color 0.3s ease-in-out;
  padding: 10px;
}

.icon-container:hover {
  background-color: #e0e0e0bf;
}

.persons { background-color: #87CEEB; } 
.products { background-color: #98FB98; } 
.sales { background-color: #FFD700; } 

.icons-container .icon {
  font-size: 4em;
  margin-bottom: 10px;
}

span {
  display: block;
  margin-top: 5px;
}
</style>

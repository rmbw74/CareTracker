
//This eventlistener fires off when a user clicks on the add dependents button and redirects to the Dependents Create URL
$("#AddDependentButton").on("click", evt => {
    console.log("You Clicked")
    evt.preventDefault()
    window.location = `/Dependents/Create`
})

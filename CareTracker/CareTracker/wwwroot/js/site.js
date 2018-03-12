$("#AddDependentButton").on("click", evt => {
    console.log("You Clicked")
    evt.preventDefault()
    window.location = `/Dependents/Create`
})

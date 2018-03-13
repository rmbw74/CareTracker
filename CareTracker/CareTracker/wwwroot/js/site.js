
//This eventlistener fires off when a user clicks on the add dependents button and redirects to the Dependents Create URL
$("#AddDependentButton").on("click", evt => {
    console.log("You Clicked")
    evt.preventDefault()
    window.location = `/Dependents/Create`
})


//This eventlistener fires off when a user clicks on a dependent, and will show the dependent summary screen
$(".UserDependentList__Dependent").on("click", evt => {

    //split the event target id and grab the dependent id number
    const DependentId = evt.target.id.split("--")[1]
    console.log("You clicked on ", DependentId )
    //change window locations using the dependentId to show summary for dependent
    window.location = `/Summary/Show/${DependentId}`
    
})
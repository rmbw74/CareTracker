
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
    if (DependentId) {
        console.log("You clicked on ", DependentId)
        //change window locations using the dependentId to show summary for dependent
        window.location = `/Summary/Show/${DependentId}`
    }
        
    
    
})

//This eventlistener fires off when a user clicks on the Show All Doctors button on the summary page and directs the browser to 
// the doctors/showall page

$("#AppointmentActionBar").on("click", evt => {

    //split the target id and grab the dependentid
    const DependentId = evt.target.id.split("--")[1]
    const target = evt.target.id.split("--")[0]
    console.log(target)
    console.log(evt.target.id)
    if (target !== null) {
        if (target === "AllDependentAppointments") {
            console.log(target)
            window.location = `/Appointments/DependentAppointments/${DependentId}`
        } else if (target === "Doctors") {
            console.log(target)
            window.location = `/Doctors/DependentDoctors/${DependentId}`
        }
    }
})

   

//This eventlistener fires off when a user clicks on a shared user in the share tab
$("#SharedUserGrid").on("click", evt => {
    console.log("You Clicked")
    //split the id of the target click on --
    const SharedDependentId = evt.target.id.split("--")[1]
    const target = evt.target.id.split("--")[0]
    console.log("We clicked on", target + " " + SharedDependentId)
    if (target !== null)
    {
        if (target === "AcceptShare") {
            console.log(target)
            window.location = `/SharedDependents/AcceptShare/${SharedDependentId}`
        } else if (target === "RejectShare")
        {
            console.log(target)
            window.location = `/SharedDependents/RejectShare/${SharedDependentId}`
        }

    }

    console.log(target)

})

Edy's Vehicle Physics for Unity - v5.1
http://www.edy.es/dev/vehicle-physics
------------------------------------------------

UPGRADING FROM EVP 5.0:

	Compatibility-breaking changes:

	- Center Of Mass is now defined with parameters by default (Parametric).

		You can still use the previous mode by choosing "Transform" from the Mode options.
		A context-menu entry "Convert Center of Mass from Transform To Parametric" automatically
		calculates the parametric settings out of an existing CoM transform.

	- Aerodynamics have changed.

		You may need to re-configure your vehicles at high
		speeds using the new Aerodynamics balance setting (at Vehicle Balance).

	Check out the new Vehicle Balance section and the improved Driving Aids at the Vehicle
	Controller component for new ways of setting up awesome cars!

UPGRADING FROM EVP 4:

	Read the documentation at http://www.edy.es/dev/vehicle-physics


Sample scenes:

	The City - Simple Scene
	A single vehicle and a camera in the most simple scene.

	The City - Simple Scene - Drift
	The most simple scene for a car with a drift setup. Have fun!

	The City - Vehicle Manager
	Several vehicles can be selected with a vehicle manager component.


Controls:

WSAD or arrows		Throttle, brake, steering
Space				Handbrake
Enter				Reset vehicle (if it rolls over)
C					Change camera
Tab or PgUp/PgDown	Select vehicle (Vehicle Manager scene)
E					Make the gray stone to "jump" (for load tests)
R					Repair vehicle damage.
P					Pause the vehicles (Vehicle Manager scene)
						Camera and vehicle selection can be used while in Pause.
Y					Show/hide telemetry. Shift-Y for switching telemetry modes.
Esc					Restart the scene.


Head to http://www.edy.es/dev/vehicle-physics for the component documentation.
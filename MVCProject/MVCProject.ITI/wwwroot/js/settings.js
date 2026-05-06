document.addEventListener('DOMContentLoaded', function () {
    // Initialize tooltips
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Profile Form Submission
    const profileForm = document.getElementById('profileForm');
    if (profileForm) {
        profileForm.addEventListener('submit', async function (e) {
            e.preventDefault();
            
            if (!profileForm.checkValidity()) {
                e.stopPropagation();
                profileForm.classList.add('was-validated');
                return;
            }

            const formData = new FormData(profileForm);
            const data = Object.fromEntries(formData.entries());

            try {
                const response = await fetch('/Settings/UpdateProfile', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value
                    },
                    body: JSON.stringify(data)
                });

                const result = await response.json();
                
                if (result.success) {
                    showAlert('success', result.message);
                } else {
                    showAlert('danger', result.message);
                }
            } catch (error) {
                showAlert('danger', 'An error occurred while updating profile');
            }
        });
    }

    // Vehicle Form - Dropdown Logic
    const makeSelect = document.getElementById('Make');
    const modelSelect = document.getElementById('Model');
    const yearSelect = document.getElementById('Year');
    const wltpMixedInput = document.getElementById('WltpMixed');
    const fuelTypeSelect = document.getElementById('FuelType');
    const overrideBtn = document.getElementById('overrideBtn');
    const isOverrideInput = document.getElementById('IsOverride');

    // Load Models when Make changes
    if (makeSelect) {
        makeSelect.addEventListener('change', async function () {
            const make = this.value;
            
            // Reset Model and Year
            modelSelect.innerHTML = '<option value="">Select Model</option>';
            yearSelect.innerHTML = '<option value="">Select Year</option>';
            
            if (make) {
                try {
                    const response = await fetch(`/Settings/GetModels?make=${encodeURIComponent(make)}`);
                    const models = await response.json();
                    
                    models.forEach(model => {
                        const option = document.createElement('option');
                        option.value = model;
                        option.textContent = model;
                        modelSelect.appendChild(option);
                    });
                } catch (error) {
                    console.error('Error loading models:', error);
                }
            }
        });
    }

    // Load Years when Model changes
    if (modelSelect) {
        modelSelect.addEventListener('change', async function () {
            const make = makeSelect.value;
            const model = this.value;
            
            // Reset Year
            yearSelect.innerHTML = '<option value="">Select Year</option>';
            
            if (make && model) {
                try {
                    const response = await fetch(`/Settings/GetYears?make=${encodeURIComponent(make)}&model=${encodeURIComponent(model)}`);
                    const years = await response.json();
                    
                    years.forEach(year => {
                        const option = document.createElement('option');
                        option.value = year;
                        option.textContent = year;
                        yearSelect.appendChild(option);
                    });
                } catch (error) {
                    console.error('Error loading years:', error);
                }
            }
        });
    }

    // Auto-fill WLTP data when Year changes
    if (yearSelect) {
        yearSelect.addEventListener('change', async function () {
            const make = makeSelect.value;
            const model = modelSelect.value;
            const year = this.value;
            
            if (make && model && year) {
                try {
                    const response = await fetch(`/Settings/GetCarModel?make=${encodeURIComponent(make)}&model=${encodeURIComponent(model)}&year=${year}`);
                    const result = await response.json();
                    
                    if (result.success) {
                        wltpMixedInput.value = result.wltpMixed;
                        fuelTypeSelect.value = result.fuelType;
                        wltpMixedInput.readOnly = true;
                        overrideBtn.innerHTML = '<i class="bi bi-pencil"></i> Override';
                        isOverrideInput.value = 'false';
                    }
                } catch (error) {
                    console.error('Error loading WLTP data:', error);
                }
            }
        });
    }

    // Override button functionality
    if (overrideBtn) {
        overrideBtn.addEventListener('click', function () {
            if (wltpMixedInput.readOnly) {
                wltpMixedInput.readOnly = false;
                wltpMixedInput.focus();
                overrideBtn.innerHTML = '<i class="bi bi-x-lg"></i> Cancel';
                isOverrideInput.value = 'true';
            } else {
                wltpMixedInput.readOnly = true;
                overrideBtn.innerHTML = '<i class="bi bi-pencil"></i> Override';
                isOverrideInput.value = 'false';
            }
        });
    }

    // Vehicle Form Submission
    const vehicleForm = document.getElementById('vehicleForm');
    if (vehicleForm) {
        vehicleForm.addEventListener('submit', async function (e) {
            e.preventDefault();
            
            if (!vehicleForm.checkValidity()) {
                e.stopPropagation();
                vehicleForm.classList.add('was-validated');
                return;
            }

            const formData = new FormData(vehicleForm);
            const data = Object.fromEntries(formData.entries());
            data.WltpMixed = parseFloat(data.WltpMixed);
            data.Year = parseInt(data.Year);
            data.PassengerCapacity = parseInt(data.PassengerCapacity);
            data.IsOverride = isOverrideInput.value === 'true';

            try {
                const response = await fetch('/Settings/UpdateVehicleInfo', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value
                    },
                    body: JSON.stringify(data)
                });

                const result = await response.json();
                
                if (result.success) {
                    showAlert('success', result.message);
                } else {
                    showAlert('danger', result.message);
                }
            } catch (error) {
                showAlert('danger', 'An error occurred while updating vehicle info');
            }
        });
    }

    // Delete Account Modal
    const deleteAccountBtn = document.getElementById('deleteAccountBtn');
    const deleteAccountModal = new bootstrap.Modal(document.getElementById('deleteAccountModal'));
    const confirmDeleteBtn = document.getElementById('confirmDeleteBtn');

    if (deleteAccountBtn) {
        deleteAccountBtn.addEventListener('click', function () {
            deleteAccountModal.show();
        });
    }

    if (confirmDeleteBtn) {
        confirmDeleteBtn.addEventListener('click', async function () {
            try {
                const response = await fetch('/Settings/DeleteAccount', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value
                    }
                });

                const result = await response.json();
                
                if (result.success) {
                    deleteAccountModal.hide();
                    showAlert('success', result.message);
                    setTimeout(() => {
                        window.location.href = result.redirectUrl || '/';
                    }, 1500);
                } else {
                    showAlert('danger', result.message);
                }
            } catch (error) {
                showAlert('danger', 'An error occurred while deleting account');
            }
        });
    }

    // Alert Helper Function
    function showAlert(type, message) {
        // Remove existing alerts
        const existingAlerts = document.querySelectorAll('.alert-dismissible');
        existingAlerts.forEach(alert => alert.remove());

        const alertDiv = document.createElement('div');
        alertDiv.className = `alert alert-${type} alert-dismissible fade show position-fixed`;
        alertDiv.style.cssText = 'top: 20px; right: 20px; z-index: 9999; min-width: 300px;';
        alertDiv.innerHTML = `
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        `;
        
        document.body.appendChild(alertDiv);
        
        setTimeout(() => {
            alertDiv.remove();
        }, 4000);
    }
});

WeakReference weakNavigationController;
  	WeakReference weakLoadingOverlay;


		UINavigationController navigationController
		{
			get
			{
				if (weakNavigationController == null || !weakNavigationController.IsAlive)
					return null;

				return weakNavigationController.Target as UINavigationController;
			}
		}
		LoadingOverlay loadingOverlay
		{
			get
			{
				if (weakLoadingOverlay == null || !weakLoadingOverlay.IsAlive)
					return null;

				return weakLoadingOverlay.Target as LoadingOverlay;
			}
		}
    
    public override void ViewWillAppear (bool animated)
  	{
			InvokeOnMainThread (() => {
				if(navigationController != null)
				{
					navigationController.NavigationItem.SetHidesBackButton (false, true);  
					navigationController.NavigationBar.Hidden = false;
				}
				using (MenuController theMenu = new MenuController(this.NavigationController)) {
					NavigationItem.RightBarButtonItem = new UIBarButtonItem ("Open menu", UIBarButtonItemStyle.Bordered, theMenu.triggerDialog);
				}
				weakLoadingOverlay = new WeakReference(new LoadingOverlay (UIScreen.MainScreen.Bounds)); 
				if (weakLoadingOverlay.IsAlive) {
					var loadingOverlay = weakLoadingOverlay.Target as LoadingOverlay;
					View.Add (loadingOverlay);
					loadingOverlay.Hide ();
				} 
			}); 
		}

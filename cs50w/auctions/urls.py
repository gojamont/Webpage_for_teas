from django.urls import path

from . import views

urlpatterns = [
    path("", views.index, name="index"),
    path("login", views.login_view, name="login"),
    path("logout", views.logout_view, name="logout"),
    path("register", views.register, name="register"),
    path("create", views.create, name="create"),
    path("listing/<int:listing_id>", views.listing_view, name="listing"),
    path("listing/<int:listing_id>/add", views.add, name="add"),
    path("listing/<int:listing_id>/remove", views.remove, name="remove"),
    path("watchlist", views.watchlist_view, name="watchlist"),
    path("categories/", views.category_view, name="categories"),
    path("categories/<int:category_id>", views.category_page, name="category"),
    path("listing/<int:listing_id>/bid", views.bid, name="bid"), 
    path("listing/<int:listing_id>/close", views.close, name="close"), 
    path("listing/<int:listing_id>/comment", views.add_comment, name="add_comment")
]

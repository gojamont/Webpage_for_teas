from django.contrib import admin
from .models import User, listings, bids, comments, Category

# Register your models here.
admin.site.register(User)
admin.site.register(listings)
admin.site.register(bids)
admin.site.register(comments)
admin.site.register(Category)



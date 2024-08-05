from django.contrib.auth.models import AbstractUser
from django.db import models
from django.core.validators import MinValueValidator
import datetime


class User(AbstractUser):
    pass

class Category(models.Model):
    name = models.CharField(max_length=100)

    def __str__(self):
        return self.name
    
# a model for auction listings
class listings(models.Model):
    title = models.CharField(max_length=80)
    description = models.TextField()
    price = models.FloatField()
    current_price = models.FloatField()
    active = models.BooleanField(default=True)
    url = models.URLField(max_length=500, blank=True, default='')
    user_owner = models.ForeignKey(User, on_delete=models.CASCADE, blank=True, null=True, related_name="user")
    category = models.ForeignKey(Category, on_delete=models.CASCADE, blank=True, null=True, related_name="category")
    watchlist = models.ManyToManyField(User, blank=True, null=True, related_name="userwatchlist")
    user_winner = models.ForeignKey(User, on_delete=models.CASCADE, blank=True, null=True, related_name="user_winner")
    
    if TypeError:
        pass

    def __str__(self):
        return self.title

    # a model for bids
class bids(models.Model):
    current_bid = models.FloatField(blank=True, null=False)
    listing_bid = models.ForeignKey(listings, on_delete=models.CASCADE, blank=True, null=True, related_name="listing_bid")
    user_bid = models.ForeignKey(User, on_delete=models.CASCADE, blank=True, null=True, related_name="user_bid")

    def __str__(self):
        return str(self.current_bid)

# a model for comments
class comments(models.Model):
    user = models.ForeignKey(User, on_delete=models.CASCADE, blank=True, null=True, related_name="user_comment")
    date_posted = models.DateTimeField(default=datetime.datetime.now())
    comment = models.CharField(default="No comments", max_length=1000)
    listing_id = models.ForeignKey(listings, on_delete=models.CASCADE, blank=True, null=True, related_name="listing_comment")
    
    def __str__(self):
        return f"{self.date_posted.strftime('%Y-%m-%d %H:%M')}"
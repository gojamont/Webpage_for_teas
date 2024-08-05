from django.contrib.auth import authenticate, login, logout
from django.db import IntegrityError
from django.http import HttpResponse, HttpResponseRedirect
from django.shortcuts import render
from django.urls import reverse
from django.db.models import Max
from django.contrib.auth.decorators import login_required

from .models import User, listings, bids, comments, Category

def get_highest_bid(listing_id):
    listing = listings.objects.get(pk=listing_id)
    
    #bids section
    max_bid = bids.objects.filter(listing_bid=listing_id)
    max_bid = max_bid.aggregate(Max('current_bid'))
    current_price = max_bid['current_bid__max']
    
    if current_price == None:
        current_price = listing.price
    
    listing.current_price = current_price
    listing.save()

    return current_price

def index(request):
    AllListings=listings.objects.all()
    return render(request, "auctions/index.html", {
        "listings": AllListings
    })


# a page for listing
def listing_view(request, listing_id):
    listing = listings.objects.get(pk=listing_id)
    currentUser = request.user
    #watchlist
    inWatchlist = currentUser in listing.watchlist.all()
    current_price = get_highest_bid(listing.id)
    # comment section 
    Allcomments = comments.objects.filter(listing_id=listing_id).all()
    
    return render(request, "auctions/listing_page.html", {
        "listing": listing,
        "inWatchlist":inWatchlist,
        "max_bid" : current_price, 
        "user_winner" : listing.user_winner, 
        "comments" : Allcomments
    })

# a function to add listing to a watchlist
@login_required
def add(request, listing_id):
    listing = listings.objects.get(pk=listing_id)
    user = request.user
    listing.watchlist.add(user)
    return HttpResponseRedirect(reverse("listing", args=(listing.id, )))

@login_required
def remove(request, listing_id):
    listing = listings.objects.get(pk=listing_id)
    user = request.user
    listing.watchlist.remove(user)
    return HttpResponseRedirect(reverse("listing", args=(listing.id, )))

@login_required
def watchlist_view(request):
    listings = request.user.userwatchlist.all()
    return render(request, "auctions/watchlist.html", {
        "listings" : listings
    })

def category_view(request):
    categories=Category.objects.all()
    all_listings = listings.objects.all()
    return render(request, "auctions/category_page.html", {
            "categories" : categories, 
            "listings": all_listings,
        })
    
def category_page(request, category_id):
    category = Category.objects.get(pk=category_id)
    filtered_listing = listings.objects.filter(category=category)
    return render(request, "auctions/category_view.html", {
            "category" : category,
            "listings": filtered_listing
        })

def login_view(request):
    if request.method == "POST":
        # Attempt to sign user in
        username = request.POST["username"]
        password = request.POST["password"]
        user = authenticate(request, username=username, password=password)

        # Check if authentication successful
        if user is not None:
            login(request, user)
            return HttpResponseRedirect(reverse("index"))
        else:
            return render(request, "auctions/login.html", {
                "message": "Invalid username and/or password."
            })
    else:
        return render(request, "auctions/login.html")

#add a comment
@login_required
def add_comment(request, listing_id):
    currentUser = request.user
    listing = listings.objects.get(pk=listing_id)
    if request.method == "POST":
        comment = request.POST["comment"]
        new_comment = comments (
            user = currentUser,
            comment = comment,
            listing_id = listing
        )
        new_comment.save()
    return HttpResponseRedirect(reverse("listing", args=(listing.id, )))

    
#add a bid
@login_required
def bid(request, listing_id):
    listing = listings.objects.get(pk=listing_id)
    Allcomments = comments.objects.filter(listing_id=listing_id).all()
    currentUser = request.user
    
    if request.method == "POST":
        # saves a bit to the data base (model)
        bid = request.POST["bid"]
        new_bid = bids(
            current_bid = float(bid), 
            user_bid = currentUser, 
            listing_bid = listing
        )
        
        try:
            #saving bids
            if float(new_bid.current_bid) < listing.price:
                    return render(request, "auctions/listing_page.html", {
                        "failure": "Bid should be greater than starting price",
                        "listing": listing,
                        "max_bid": get_highest_bid(listing_id), 
                        "comments":Allcomments
                    })
            if new_bid.current_bid <= float(listing.current_price) and listing.current_price is not None:
                return render(request, "auctions/listing_page.html", {
                            "failure": "Bid should be greater than any other value placed",
                            "listing": listing, 
                            "max_bid": get_highest_bid(listing_id),
                            "comments":Allcomments
                        })
        except:
            pass
        
        new_bid.save()
        
        return render(request, "auctions/listing_page.html", {
           "success": "Bid has been placed successfully",
           "listing": listing,
           "max_bid": new_bid.current_bid, 
           "comments": Allcomments})
        
    return HttpResponseRedirect(reverse("listing", args=(listing.id, )))

#close auction function
@login_required
def close(request, listing_id):
    listing = listings.objects.get(pk=listing_id)
    user_winner = bids.objects.all().filter(listing_bid=listing_id).order_by('-current_bid').values('user_bid').first()
    user_winner = User.objects.get(pk=user_winner["user_bid"])
    
    if request.method == "POST":
        if listing.active == True:
            listing.active = False
            listing.user_winner = user_winner
            listing.save()
            
        return render(request, "auctions/listing_page.html", {
            "listing" : listing,
            "max_bid": get_highest_bid(listing_id), 
            "user_winner" : user_winner, 
            "success": f"Winner is {user_winner}"})
        
    return HttpResponseRedirect(reverse("listing", args=(listing.id, )))
    
def logout_view(request):
    logout(request)
    return HttpResponseRedirect(reverse("index"))


def register(request):
    if request.method == "POST":
        username = request.POST["username"]
        email = request.POST["email"]

        # Ensure password matches confirmation
        password = request.POST["password"]
        confirmation = request.POST["confirmation"]
        if password != confirmation:
            return render(request, "auctions/register.html", {
                "message": "Passwords must match."
            })

        # Attempt to create new user
        try:
            user = User.objects.create_user(username, email, password)
            user.save()
        except IntegrityError:
            return render(request, "auctions/register.html", {
                "message": "Username already taken."
            })
        login(request, user)
        return HttpResponseRedirect(reverse("index"))
    else:
        return render(request, "auctions/register.html")
    
#create a new listing
def create(request):
    if request.method == "POST":
        title = request.POST["title"]
        description = request.POST["description"]
        price = request.POST["price"]
        category = request.POST["category"]
        url = request.POST.get('url')

        owning_user = request.user
        categoryinfo = Category.objects.get(name=category)

        user_listing = listings(
            title=title,
            description=description,
            price=float(price),
            current_price=float(price),
            url=url,
            category=categoryinfo,
            user_owner=owning_user
        )
        #Saving a listing
        user_listing.save()

        #returning back to index
        return HttpResponseRedirect(reverse("index"))
    
    #Rendering a page
    categories=Category.objects.all()
    return render(request, "auctions/create_listing.html", {
        "categories": categories
    })

    
    